using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MiniProject2.Connectors;
using MiniProject2.ServiceBook.Protos;

namespace MiniProject2.ServiceBook
{
    public class BookService : BookProto.BookProtoBase
    {
        private readonly ILogger<BookService> _logger;
        public BookService(ILogger<BookService> logger)
        {
            _logger = logger;
        }

        public override Task<BoolValue> ISBN13Validator(StringValue str, ServerCallContext context)
        {
            BoolValue flagValue = new BoolValue();
            flagValue.Value = SoapConnector.ISBN13Validator(str.Value).Result;
            return Task.FromResult(flagValue);
        }
        public override Task<BoolValue> ISBN10Validator(StringValue str, ServerCallContext context)
        {
            BoolValue flagValue = new BoolValue();
            flagValue.Value = SoapConnector.ISBN10Validator(str.Value).Result;
            return Task.FromResult(flagValue);
        }
    }

}