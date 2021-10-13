using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using MiniProject2.Models.DTO;
using MiniProject2.Models.Managers;
using MiniProject2.Factory.Protos;

namespace MiniProject2.Factory.Clients
{
    public class ExamClient
    {
        public static async Task<ExamDTO> GetExamByIdAsync(int id)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://serviceexam:80");
            var client = new ExamProto.ExamProtoClient(channel);

            var t = new Int64Value();
            t.Value = id;

            ExamObj reply = await client.GetExamByIdAsync(t);
            return ProtoMapper<ExamObj, ExamDTO>.Map(reply);
        }

        public static async Task<List<ExamDTO>> GetExamsAsync()
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://serviceexam:80");
            var client = new ExamProto.ExamProtoClient(channel);

            AllExamsReply reply = await client.GetAllExamsAsync(new Empty());
            List<ExamDTO> MappedList = new List<ExamDTO>();

            foreach (var exam in reply.Exams)
            {
                MappedList.Add(ProtoMapper<ExamObj, ExamDTO>.Map(exam));
            }
            
            return MappedList;
        }
        public static async Task<ExamDTO> AddExamAsync(AddExamDTO exam)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://serviceexam:80");
            var client = new ExamProto.ExamProtoClient(channel);

            ExamObj s = await client.AddExamAsync(ProtoMapper<AddExamDTO, ExamObj>.Map(exam));
            
            return ProtoMapper<ExamObj, ExamDTO>.Map(s);
        }
    }
}