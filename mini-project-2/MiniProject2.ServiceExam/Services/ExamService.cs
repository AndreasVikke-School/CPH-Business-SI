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
                Exam s = dbContext.Exams.Where(x => x.Id == id.Value).Single();
                return Task.FromResult(ProtoMapper<Exam, ExamObj>.Map(s));
            }
        }
        public override Task<AllExamsReply> GetAllExams(Empty empty, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                List<Exam> Exams = dbContext.Exams.ToList();
                AllExamsReply reply = new AllExamsReply{};
                Exams.ForEach(s => reply.Exams.Add(
                    ProtoMapper<Exam, ExamObj>.Map(s)));
                return Task.FromResult(reply);
            }
        }
        public override Task<ExamObj> AddExam(ExamObj Exam, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                Exam s = ProtoMapper<ExamObj, Exam>.Map(Exam);
                dbContext.Exams.Add(s);
                dbContext.SaveChanges();
                return Task.FromResult(ProtoMapper<Exam, ExamObj>.Map(s));
            }
        }
    }
}
