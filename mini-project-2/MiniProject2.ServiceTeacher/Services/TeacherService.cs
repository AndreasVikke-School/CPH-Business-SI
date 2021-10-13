using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MiniProject2.EF.DatabaseContexts;
using MiniProject2.Models.Managers;
using MiniProject2.Models.Models;
using MiniProject2.ServiceTeacher.Protos;

namespace MiniProject2.ServiceTeacher
{
    public class TeacherService : TeacherProto.TeacherProtoBase
    {
        private readonly ILogger<TeacherService> _logger;
        public TeacherService(ILogger<TeacherService> logger)
        {
            _logger = logger;
        }

        public override Task<AllTeachersReply> GetAllTeachers(Empty empty, ServerCallContext context)
        {
            using(var dbContext = new SchoolContext())
            {
                List<Teacher> teachers = dbContext.Teachers.ToList();
                AllTeachersReply reply = new AllTeachersReply{};
                teachers.ForEach(t => reply.Teachers.Add(
                    ProtoMapper<Teacher, TeacherObj>.Map(t)));
                return Task.FromResult(reply);
            }
        }
        public override Task<TeacherObj> GetTeacherById(Int64Value id, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                Teacher s = dbContext.Teachers.Where(x => x.Id == id.Value).SingleOrDefault();
                return Task.FromResult(ProtoMapper<Teacher, TeacherObj>.Map(s));
            }
        }
        public override Task<TeacherObj> AddTeacher(TeacherObj teacher, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                Teacher t = ProtoMapper<TeacherObj, Teacher>.Map(teacher);
                dbContext.Teachers.Add(t);
                dbContext.SaveChanges();
                return Task.FromResult(ProtoMapper<Teacher, TeacherObj>.Map(t));
            }
        }
    }
}