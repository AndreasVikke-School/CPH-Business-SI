using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using MiniProject2.Factory.Protos;

namespace MiniProject2.Factory.Clients
{
    public class BookClient
    {
        public static async Task<bool> ISBN13Validator(string isbn)
        {
            using var channel = GrpcChannel.ForAddress("http://servicebook:80");
            var client = new BookProto.BookProtoClient(channel);

            BoolValue reply = await client.ISBN13ValidatorAsync(new StringValue() { Value = isbn });
            return reply.Value;
        }

        public static async Task<bool> ISBN10Validator(string isbn)
        {
            using var channel = GrpcChannel.ForAddress("http://servicebook:80");
            var client = new BookProto.BookProtoClient(channel);
            
            var s = new StringValue();
            s.Value = isbn;

            BoolValue reply = await client.ISBN10ValidatorAsync(s);
            return reply.Value;
        }
    }
}