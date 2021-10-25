using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using MiniProject2.Models.DTO;
using MiniProject2.Models.Managers;
using MiniProject2.Factory.Protos;

namespace MiniProject2.Factory.Clients
{
    public class StudentClient
    {
        public static async Task<StudentDTO> GetStudentByIdAsync(long id)
        {
            using var channel = GrpcChannel.ForAddress("http://servicestudent:80");
            var client = new StudentProto.StudentProtoClient(channel);

            StudentObj reply = await client.GetStudentByIdAsync(new Int64Value() { Value = id });
            return ProtoMapper<StudentObj, StudentDTO>.Map(reply);
        }

        public static async Task<List<StudentDTO>> GetStudentsAsync()
        {
            using var channel = GrpcChannel.ForAddress("http://servicestudent:80");
            var client = new StudentProto.StudentProtoClient(channel);

            AllStudentsReply reply = await client.GetAllStudentsAsync(new Empty());
            List<StudentDTO> MappedList = new List<StudentDTO>();

            foreach (var student in reply.Students)
            {
                MappedList.Add(ProtoMapper<StudentObj, StudentDTO>.Map(student));
            }
            
            return MappedList;
        }
        public static async Task<StudentDTO> AddStudentAsync(AddStudentDTO student)
        {
            using var channel = GrpcChannel.ForAddress("http://servicestudent:80");
            var client = new StudentProto.StudentProtoClient(channel);

            StudentObj s = await client.AddStudentAsync(ProtoMapper<AddStudentDTO, StudentObj>.Map(student));
            
            return ProtoMapper<StudentObj, StudentDTO>.Map(s);
        }
    }
}