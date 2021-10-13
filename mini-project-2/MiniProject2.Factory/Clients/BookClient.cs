using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using MiniProject2.Factory.Protos;

namespace MiniProject2.Factory.Clients
{
    public class BookClient
    {
        public static async Task<bool> ISBN13Validator(string isbn)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://servicebook:80");
            var client = new BookProto.BookProtoClient(channel);
            
            var s = new StringValue();
            s.Value = isbn;

            BoolValue reply = await client.ISBN13ValidatorAsync(s);
            return reply.Value;
        }

        public static async Task<bool> ISBN10Validator(string isbn)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://servicebook:80");
            var client = new BookProto.BookProtoClient(channel);
            
            var s = new StringValue();
            s.Value = isbn;

            BoolValue reply = await client.ISBN10ValidatorAsync(s);
            return reply.Value;
        }
    }
}