using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using MiniProject2.Models.DTO;
using MiniProject2.Models.Managers;
using MiniProject2.Factory.Protos;

namespace MiniProject2.Factory.Clients
{
    public class StudentClient
    {
        public static async Task<StudentDTO> GetStudentByIdAsync(int id)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://servicestudent:80");
            var client = new StudentProto.StudentProtoClient(channel);

            var t = new Int64Value();
            t.Value = id;

            StudentObj reply = await client.GetStudentByIdAsync(t);
            return ProtoMapper<StudentObj, StudentDTO>.Map(reply);
        }

        public static async Task<List<StudentDTO>> GetStudentsAsync()
        {
            // The port number(5001) must match the port of the gRPC server.
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
        public static async Task<StudentDTO> AddStudentsAsync(AddStudentDTO student)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://servicestudent:80");
            var client = new StudentProto.StudentProtoClient(channel);

            StudentObj s = client.AddStudent(ProtoMapper<AddStudentDTO, StudentObj>.Map(student));
            
            return ProtoMapper<StudentObj, StudentDTO>.Map(s);
        }
    }
}