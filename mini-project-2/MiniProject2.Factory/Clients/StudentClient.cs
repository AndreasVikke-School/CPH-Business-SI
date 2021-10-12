using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using MiniProject2.Factory.DTO;
using MiniProject2.Factory.Managers;
using MiniProject2.Factory.Protos;

namespace MiniProject2.Factory.Clients
{
    public class StudentClient
    {
        public static async Task<StudentDTO> GetStudentByIdAsync(int id)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://studentservice:80");
            var client = new StudentProto.StudentProtoClient(channel);

            var t = new Int64Value();
            t.Value = 1;

            StudentObj reply = await client.GetStudentByIdAsync(t);
            return ProtoMapper<StudentObj, StudentDTO>.Map(reply);
        }
    }
}