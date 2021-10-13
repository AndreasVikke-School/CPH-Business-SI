using Grpc.Net.Client;
using MiniProject2.Models.DTO;
using MiniProject2.Models.Managers;
using MiniProject2.Factory.Protos;
using Google.Protobuf.WellKnownTypes;

namespace MiniProject2.Factory.Clients
{
    public class GradeClient
    {

        public static async Task<GradeDTO> GetGradeByIdAsync(long id)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://servicegrade:80");
            var client = new GradeProto.GradeProtoClient(channel);

            var t = new Int64Value();
            t.Value = id;

            GradeObj reply = await client.GetGradeByIdAsync(t);
            GradeDTO grade = ProtoMapper<GradeObj, GradeDTO>.Map(reply);
            grade.Student = new StudentDTO(){Id = reply.StudentId};
            grade.Exam = new ExamDTO(){Id = reply.ExamId};
            return grade; 
        }

        public static async Task<List<GradeDTO>> GetGradesAsync()
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://servicegrade:80");
            var client = new GradeProto.GradeProtoClient(channel);

            AllGradesReply reply = await client.GetAllGradesAsync(new Empty());
            List<GradeDTO> MappedList = new List<GradeDTO>();

            foreach (var grade in reply.Grades)
            {
                MappedList.Add(ProtoMapper<GradeObj, GradeDTO>.Map(grade));
            }

            return MappedList;
        }
        public static async Task<GradeDTO> AddGradeToStudentAsync(AddGradeDTO grade)
        {

            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://servicegrade:80");
            var client = new GradeProto.GradeProtoClient(channel);

            GradeObj s = client.AddGrade(ProtoMapper<AddGradeDTO, GradeObj>.Map(grade));

            return ProtoMapper<GradeObj, GradeDTO>.Map(s);
        }
    }
}