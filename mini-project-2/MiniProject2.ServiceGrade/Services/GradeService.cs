using MiniProject2.Models.Models;
using MiniProject2.Models.Models.Types;
using MiniProject2.Models.DTO;
using MiniProject2.Models.Managers;
using MiniProject2.ServiceGrade.Protos;
using MiniProject2.EF.DatabaseContexts;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
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
                grades.ForEach(s =>
                {
                    Grade grade = dbContext.Grades.Where(x => x.Id == s.Id)
                        .Include(x => x.Student)
                        .Include(x => x.Exam)
                        .Single();
                    GradeObj go = ProtoMapper<Grade, GradeObj>.Map(grade);
                    go.StudentId = grade.Student.Id;
                    go.ExamId = grade.Exam.Id;
                    reply.Grades.Add(go);
                });
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
                    .Single();
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
                s.ActualGrade = grade.ActualGrade;
                dbContext.Grades.Add(s);
                dbContext.SaveChanges();
                return Task.FromResult(ProtoMapper<Grade, GradeObj>.Map(s));
            }
        }

        public override Task<AllGradesReply> GetPassedStudents(Int64Value examId, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                List<Grade> grades = dbContext.Grades
                    .Where(x => x.Exam.Id == examId.Value)
                    .Where(x => x.ActualGrade >= 2)
                    .Include(x => x.Student)
                    .Include(x => x.Exam)
                    .ToList();
                    
                AllGradesReply reply = new AllGradesReply { };
                grades.ForEach(s =>
                {
                    GradeObj go = ProtoMapper<Grade, GradeObj>.Map(s);
                    go.StudentId = s.Student.Id;
                    go.ExamId = s.Exam.Id;
                    reply.Grades.Add(go);
                });
                return Task.FromResult(reply);
            }
        }

        public override Task<Int64Value> GetFailedStudentsAmmount(Int64Value examId, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                int failed = dbContext.Grades
                    .Where(x => x.Exam.Id == examId.Value)
                    .Where(x => x.ActualGrade < 2)
                    .Count();

                return Task.FromResult(new Int64Value() { Value = failed });
            }
        }
    }
}