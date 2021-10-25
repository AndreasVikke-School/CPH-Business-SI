using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using MiniProject2.Models.DTO;
using MiniProject2.Models.Managers;
using MiniProject2.Factory.Protos;
using MiniProject2.ServiceTeacher.Protos;

namespace MiniProject2.Factory.Clients
{
    public class TeacherClient
    {
        public static async Task<TeacherDTO> GetTeacherByIdAsync(long id)
        {
            using var channel = GrpcChannel.ForAddress("http://serviceteacher:80");
            var client = new TeacherProto.TeacherProtoClient(channel);

            TeacherObj reply = await client.GetTeacherByIdAsync(new Int64Value() { Value = id });
            return ProtoMapper<TeacherObj, TeacherDTO>.Map(reply);
        }

        public static async Task<List<TeacherDTO>> GetTeachersAsync()
        {
            using var channel = GrpcChannel.ForAddress("http://serviceteacher:80");
            var client = new TeacherProto.TeacherProtoClient(channel);

            AllTeachersReply reply = await client.GetAllTeachersAsync(new Empty());
            List<TeacherDTO> MappedList = new List<TeacherDTO>();

            foreach (var teacher in reply.Teachers)
            {
                MappedList.Add(ProtoMapper<TeacherObj, TeacherDTO>.Map(teacher));
            }
            
            return MappedList;
        }
        public static async Task<TeacherDTO> AddTeacherAsync(AddTeacherDTO teacher)
        {
            using var channel = GrpcChannel.ForAddress("http://serviceteacher:80");
            var client = new TeacherProto.TeacherProtoClient(channel);

            TeacherObj s = await client.AddTeacherAsync(ProtoMapper<AddTeacherDTO, TeacherObj>.Map(teacher));
            
            return ProtoMapper<TeacherObj, TeacherDTO>.Map(s);
        }
    }
}