using System;
using System.Collections.Generic;
using System.Linq;
using MiniProject2.StudentService.Protos;
using Google.Protobuf.WellKnownTypes;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using MiniProject2.EF.DatabaseContexts;
using MiniProject2.Models.Models;

namespace MiniProject2.StudentService
{
    public class StudentService : StudentProto.StudentProtoBase
    {
        private readonly ILogger<StudentService> _logger;
        public StudentService(ILogger<StudentService> logger)
        {
            _logger = logger;
        }

        public override Task<StudentObj> GetStudentById(Int64Value id, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                Student s = dbContext.Students.Where(x => x.Id == id.Value).SingleOrDefault();
                return Task.FromResult(new StudentObj
                {
                    Id = s.Id,
                    Name = s.Name
                });
            }
        }
    }
}
