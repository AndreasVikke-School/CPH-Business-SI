using Grpc.Net.Client;
using MiniProject2.Models.DTO;
using MiniProject2.Models.Managers;
using MiniProject2.Factory.Protos;
using Google.Protobuf.WellKnownTypes;
using static MiniProject2.Factory.Protos.GradeObj.Types;
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
            grade.ActualGrade = ProtoMapper<Grades, GradeEnumDTO>.Map(reply.Grade);
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
                g.ActualGrade = ProtoMapper<Grades, GradeEnumDTO>.Map(grade.Grade);
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
            s.Grade = ProtoMapper<GradeEnumDTO, Grades>.Map(grade.ActualGrade);

            return ProtoMapper<GradeObj, GradeDTO>.Map(s);
        }
    }
}