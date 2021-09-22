using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using MiniProject1.ClassLib.Modules;
using MiniProject1.EntityFramework;

namespace MiniProject1.Grpc
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
                    StudentObj = new StudentObj
                    {
                        Name = result.Name
                    }
                });
            }
        }

        public override Task<AllStudentsReply> GetAllStudents(Empty empty, ServerCallContext context)
        {
            using (var dcContext = new SchoolContext())
            {
                List<Student> students = dcContext.Students.ToList();

                AllStudentsReply reply = new AllStudentsReply{};
                students.ForEach(s => reply.Students.Add(new StudentReply{
                    Id = s.Id,
                    StudentObj = new StudentObj{
                        Name = s.Name
                    }
                }));

                return Task.FromResult(reply);
            }
        }
    }
}
