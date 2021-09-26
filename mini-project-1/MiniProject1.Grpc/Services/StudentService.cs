using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using MiniProject1.ClassLib.Modules;
using MiniProject1.Grpc.DatabaseContexts;
using MiniProject1.Grpc.Protos;

namespace MiniProject1.Grpc.Services
{
    public class StudentService : StudentProto.StudentProtoBase
    {
        private readonly ILogger<StudentService> _logger;
        public StudentService(ILogger<StudentService> logger)
        {
            _logger = logger;
        }

        public override Task<StudentReply> GetStudentById(StudentRequest request, ServerCallContext context)
        {
            using(var dcContext = new SchoolContext()) {
                Student result = dcContext.Students.Where(s => s.Id == request.Id).FirstOrDefault();

                return Task.FromResult(new StudentReply
                {
                    Id = result.Id,
                    StudentObj = ProtoMapper<Student, StudentObj>.Map(result)
                });
            }
        }

        public override Task<AllStudentsReply> GetAllStudents(Empty empty, ServerCallContext context)
        {
            using (var dcContext = new SchoolContext())
            {
                List<Student> students = dcContext.Students.ToList();

                AllStudentsReply reply = new AllStudentsReply{};
                students.ForEach(s => reply.Students.Add(
                    ProtoMapper<Student, StudentObj>.Map(s)
                ));
                return Task.FromResult(reply);
            }
        }

        public override Task<StudentReply> AddStudent(StudentReply input, ServerCallContext context) {
            using (var dbContext = new SchoolContext()) {
                    Student student = ProtoMapper<StudentObj, Student>.Map(input.StudentObj);
                    // foreach(CourseObj c in input.StudentObj.Courses) {
                    //     dbContext.Courses.Add(ProtoMapper<CourseObj, Course>.Map(c));
                    // }

                    dbContext.Students.Add(student);
                    dbContext.SaveChanges();

                    return Task.FromResult(new StudentReply {
                    Id = student.Id,
                    StudentObj = ProtoMapper<Student, StudentObj>.Map(student)
                    });
                
            }
        }
    }
}
