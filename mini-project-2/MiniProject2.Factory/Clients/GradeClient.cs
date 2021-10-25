using Grpc.Net.Client;
using MiniProject2.Models.DTO;
using MiniProject2.Models.Managers;
using MiniProject2.Models.Models.Types;
using MiniProject2.Factory.Protos;
using Google.Protobuf.WellKnownTypes;
using MiniProject2.Factory.DTO.Types;

namespace MiniProject2.Factory.Clients
{
    public class GradeClient
    {

        public static async Task<GradeDTO> GetGradeByIdAsync(long id)
        {
            using var channel = GrpcChannel.ForAddress("http://servicegrade:80");
            var client = new GradeProto.GradeProtoClient(channel);

            GradeObj reply = await client.GetGradeByIdAsync(new Int64Value() { Value = id });
            GradeDTO grade = ProtoMapper<GradeObj, GradeDTO>.Map(reply);
            grade.Student = new StudentDTO(){Id = reply.StudentId};
            grade.Exam = new ExamDTO(){Id = reply.ExamId};
            return grade; 
        }

        public static async Task<List<GradeDTO>> GetGradesAsync()
        {
            using var channel = GrpcChannel.ForAddress("http://servicegrade:80");
            var client = new GradeProto.GradeProtoClient(channel);

            AllGradesReply reply = await client.GetAllGradesAsync(new Empty());
            List<GradeDTO> MappedList = new List<GradeDTO>();

            foreach (var grade in reply.Grades)
            {
                GradeDTO g = ProtoMapper<GradeObj, GradeDTO>.Map(grade);
                g.Student = new StudentDTO() { Id = grade.StudentId };
                g.Exam = new ExamDTO() { Id = grade.ExamId };
                MappedList.Add(g);
            }

            return MappedList;
        }
        public static async Task<GradeDTO> AddGradeToStudentAsync(AddGradeDTO grade)
        {
            using var channel = GrpcChannel.ForAddress("http://servicegrade:80");
            var client = new GradeProto.GradeProtoClient(channel);

            GradeObj s = await client.AddGradeAsync(ProtoMapper<AddGradeDTO, GradeObj>.Map(grade));

            return ProtoMapper<GradeObj, GradeDTO>.Map(s);
        }

        public static async Task<List<GradeDTO>> getPassedStudentsAsync()
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://servicegrade:80");
            var client = new GradeProto.GradeProtoClient(channel);

            AllGradesReply reply = await client.GetPassedStudentsAsync(new Empty());
            List<GradeDTO> MappedList = new List<GradeDTO>();

            foreach (var grade in reply.Grades)
            {
                GradeDTO dto = ProtoMapper<GradeObj, GradeDTO>.Map(grade);
                dto.Student = new StudentDTO() { Id = grade.StudentId };
                dto.Exam = new ExamDTO() { Id = grade.ExamId };
                MappedList.Add(dto);
            }

            return MappedList;
        }

        public static async Task<long> getFailedStudentsAsync()
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://servicegrade:80");
            var client = new GradeProto.GradeProtoClient(channel);

            Int64Value reply = await client.GetFailedStudentsAmmountAsync(new Empty());
            long ammount = reply.Value;
            return ammount;
        }
    }
}