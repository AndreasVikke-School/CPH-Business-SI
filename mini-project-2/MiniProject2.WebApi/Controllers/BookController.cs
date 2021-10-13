using Microsoft.AspNetCore.Mvc;
using MiniProject2.Factory;
using MiniProject2.Models.DTO;

namespace MiniProject2.WebApi.Controllers
{

  [ApiController]
  [Route("/books")]
  public class BookController : ControllerBase
  {
      private readonly ILogger<BookController> _logger;
      public BookController(ILogger<BookController> logger)
      {
          _logger = logger;
      }

      [HttpGet("isbn13/{isbn}")]
      public async Task<bool> ISBN13Validator(string isbn)
      {
          return await BookFactory.ISBN13Validator(isbn);
      }

      [HttpGet("isbn10/{isbn}")]
      public async Task<bool> ISBN10Validator(string isbn)
      {
          return await BookFactory.ISBN10Validator(isbn);
      }
  }
}