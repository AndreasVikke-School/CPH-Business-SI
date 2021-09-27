using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiniProject1.ClassLib.Modules;
using MiniProject1.Grpc.DatabaseContexts;
using MiniProject1.Grpc.Protos;

namespace MiniProject1.Grpc.Services
{
    public class BookService : BookProto.BookProtoBase
    {

        private readonly ILogger<BookService> _logger;
        public BookService(ILogger<BookService> logger)
        {
            _logger = logger;
        }

        public override Task<AllBooksReply> GetAllBooks(Empty empty, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                List<Book> books = dbContext.Books.ToList();

                AllBooksReply reply = new AllBooksReply { };
                books.ForEach(b => reply.Books.Add(
                    ProtoMapper<Book, BookObj>.Map(b)
                ));

                return Task.FromResult(reply);
            }
        }
    }
}