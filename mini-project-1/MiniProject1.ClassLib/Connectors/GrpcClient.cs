using System;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace MiniProject1.ClassLib
{
    public class GrpcClient
    {
        public static async Task Run()
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client =  new StudentProto.StudentProtoClient(channel);
            var reply = await client.GetStudentByIdAsync(
                              new StudentRequest { Id = 1 });
            Console.WriteLine(reply);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public static async Task Run2()
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new StudentProto.StudentProtoClient(channel);
            var reply = await client.GetAllStudentsAsync(new Google.Protobuf.WellKnownTypes.Empty());
            Console.WriteLine(reply);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
