using System;
using System.Collections.Generic;
using System.Linq;
using MiniProject2.ServiceExam.Protos;
using Google.Protobuf.WellKnownTypes;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using MiniProject2.EF.DatabaseContexts;
using MiniProject2.Models.Models;
using MiniProject2.Models.Managers;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace MiniProject2.ServiceExam
{
    public class ExamService : ExamProto.ExamProtoBase
    {
        private readonly ILogger<ExamService> _logger;
        public ExamService(ILogger<ExamService> logger)
        {
            _logger = logger;
        }

        public override Task<ExamObj> GetExamById(Int64Value id, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                Exam e = dbContext.Exams.Where(x => x.Id == id.Value)
                .Include(x => x.Students)
                .Single();

                MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DateTime, Timestamp>();
                    cfg.CreateMap<Exam, ExamObj>();
                });
                IMapper iMapper = config.CreateMapper();


                ExamObj eo = iMapper.Map<Exam, ExamObj>(e);
                // eo.StudentId.Clear();
                foreach(var stud in e.Students) {
                    eo.StudentIds.Add(stud.Id);
                }
                eo.Date = Timestamp.FromDateTime(e.Date.ToUniversalTime());
                
                // ExamObj eo = new ExamObj{Id = e.Id, Name = e.Name, StudentId = studentIds};
                // eo.StudentId = studentIds;
                return Task.FromResult(eo);
            }
        }
        public override Task<AllExamsReply> GetAllExams(Empty empty, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DateTime, Timestamp>();
                    cfg.CreateMap<Exam, ExamObj>();
                });
                IMapper iMapper = config.CreateMapper();


                List<Exam> Exams = dbContext.Exams.ToList();
                AllExamsReply reply = new AllExamsReply{};
                Exams.ForEach(s => reply.Exams.Add(
                    iMapper.Map<Exam, ExamObj>(s)));
                return Task.FromResult(reply);
            }
        }
        public override Task<ExamObj> AddExam(ExamObj exam, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Timestamp, DateTime>();
                    cfg.CreateMap<DateTime, Timestamp>();
                    cfg.CreateMap<ExamObj, Exam>();
                    cfg.CreateMap<Exam, ExamObj>();
                });
                IMapper iMapper = config.CreateMapper();

                Exam s = iMapper.Map<ExamObj, Exam>(exam);
                s.Date = exam.Date.ToDateTime();

                s.Students = new List<Student>();
                foreach (var sid in exam.StudentIds){
                    s.Students.Add(dbContext.Students.Where(x => x.Id == sid).SingleOrDefault());
                }

                dbContext.Exams.Add(s);
                dbContext.SaveChanges();

                ExamObj eobj = iMapper.Map<Exam, ExamObj>(s);
                s.Students.ForEach(stud => eobj.StudentIds.Add(stud.Id));
                eobj.Date = exam.Date;

                return Task.FromResult(eobj);
            }
        }
    }
}
