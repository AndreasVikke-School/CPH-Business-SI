using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniProject1.ClassLib;
using MiniProject1.ClassLib.Modules;

namespace MiniProject1.WebApi.Controllers
{
    [ApiController]
    [Route("/book")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get/{id}")]
        public async Task<Book> GetBookById(string id)
        {
            return await GrpcClient.GetBook(id);
        }

        [HttpGet("get/all")]
        public async Task<List<Book>> GetAllBooks()
        {
            return await GrpcClient.GetAllBooks();
        }
    }
}
