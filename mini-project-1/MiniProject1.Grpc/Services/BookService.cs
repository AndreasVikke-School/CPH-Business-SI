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

        public override Task<BookReply> GetBookById(BookRequest request, ServerCallContext context)
        {
            using (var dcContext = new SchoolContext())
            {

                Book result = dcContext.Books.Where(b => b.ISBN == request.Isbn).FirstOrDefault();

                if (result == null)
                {
                    throw new RpcException(new Status(StatusCode.PermissionDenied, "ISBN not valid."));
                }

                return Task.FromResult(new BookReply
                {
                    Isbn = result.ISBN,
                    BookObj = ProtoMapper<Book, BookObj>.Map(result)
                });


            }
        }

        public override Task<BookReply> AddBook(BookReply input, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                Book book = ProtoMapper<BookObj, Book>.Map(input.BookObj);

                dbContext.Books.Add(book);
                dbContext.SaveChanges();

                return Task.FromResult(new BookReply
                {
                    Isbn = book.ISBN,
                    BookObj = ProtoMapper<Book, BookObj>.Map(book)
                });
            }
        }

    }
}