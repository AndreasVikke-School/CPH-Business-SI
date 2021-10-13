using MiniProject2.Models.Models;
using MiniProject2.Models.DTO;
using MiniProject2.Models.Managers;
using MiniProject2.ServiceGrade.Protos;
using MiniProject2.EF.DatabaseContexts;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace MiniProject2.ServiceGrade.Services
{
    public class GradeService : GradeProto.GradeProtoBase
    {

        public override Task<AllGradesReply> GetAllGrades(Empty empty, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                List<Grade> grades = dbContext.Grades.ToList();
                AllGradesReply reply = new AllGradesReply { };
                grades.ForEach(s => reply.Grades.Add(
                    ProtoMapper<Grade, GradeObj>.Map(s)));
                return Task.FromResult(reply);
            }
        }

        public override Task<GradeObj> GetGradeById(Int64Value id, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                Grade s = dbContext.Grades.Where(x => x.Id == id.Value)
                    .Include(x => x.Student)
                    .Include(x => x.Exam)
                    .SingleOrDefault();
                GradeObj go = ProtoMapper<Grade, GradeObj>.Map(s);
                go.StudentId = s.Student.Id;
                go.ExamId = s.Exam.Id;
                return Task.FromResult(go);
            }
        }
        public override Task<GradeObj> AddGrade(GradeObj grade, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                Grade s = ProtoMapper<GradeObj, Grade>.Map(grade);
                s.Student = dbContext.Students.Where(x => x.Id == grade.StudentId).SingleOrDefault();
                s.Exam = dbContext.Exams.Where(x => x.Id == grade.ExamId).SingleOrDefault();
                dbContext.Grades.Add(s);
                dbContext.SaveChanges();
                return Task.FromResult(ProtoMapper<Grade, GradeObj>.Map(s));
            }
        }
    }
}