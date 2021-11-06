using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using MiniProject2.Models.DTO;
using MiniProject2.Models.Managers;
using MiniProject2.Factory.Protos;
using AutoMapper;

namespace MiniProject2.Factory.Clients
{
    public class ExamClient
    {
        public static async Task<ExamDTO> GetExamByIdAsync(long id)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://serviceexam:80");
            var client = new ExamProto.ExamProtoClient(channel);

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Timestamp, DateTime>();
                cfg.CreateMap<ExamObj, ExamDTO>();
            });
            IMapper iMapper = config.CreateMapper();

            ExamObj reply = await client.GetExamByIdAsync(new Int64Value() { Value = id });
            ExamDTO exam = iMapper.Map<ExamObj, ExamDTO>(reply);

            List<StudentDTO> students = new List<StudentDTO>();

            foreach(long studId in reply.StudentIds){
                students.Add(new StudentDTO(){Id = studId});
            }
            exam.Students = students;
            exam.Date = reply.Date.ToDateTime().ToUniversalTime();
            return exam;
        }

        public static async Task<List<ExamDTO>> GetExamsAsync()
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://serviceexam:80");
            var client = new ExamProto.ExamProtoClient(channel);

            AllExamsReply reply = await client.GetAllExamsAsync(new Empty());
            List<ExamDTO> MappedList = new List<ExamDTO>();

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Timestamp, DateTime>();
                cfg.CreateMap<ExamObj, ExamDTO>();
            });
            IMapper iMapper = config.CreateMapper();

            foreach (var exam in reply.Exams)
            {
                MappedList.Add(iMapper.Map<ExamObj, ExamDTO>(exam));
            }
            
            return MappedList;
        }
        public static async Task<ExamDTO> AddExamAsync(AddExamDTO exam)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://serviceexam:80");
            var client = new ExamProto.ExamProtoClient(channel);

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DateTime, Timestamp>();
                cfg.CreateMap<Timestamp, DateTime>();
                cfg.CreateMap<AddExamDTO, ExamObj>();
                cfg.CreateMap<ExamObj, ExamDTO>();
            });
            IMapper iMapper = config.CreateMapper();

            ExamObj s = iMapper.Map<AddExamDTO, ExamObj>(exam);
            s.Date = Timestamp.FromDateTime(exam.Date.ToUniversalTime());

            ExamObj examObj = await client.AddExamAsync(s);
            ExamDTO dto = iMapper.Map<ExamObj, ExamDTO>(examObj);
            dto.Date = examObj.Date.ToDateTime();

            return dto;
        }
    }
}