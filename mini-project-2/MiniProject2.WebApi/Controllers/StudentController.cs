using Microsoft.AspNetCore.Mvc;
using MiniProject2.Factory;

namespace MiniProject2.WebApi.Controllers
{

  [ApiController]
  [Route("/students")]
  public class StudentController : ControllerBase
  {
      private readonly ILogger<StudentController> _logger;
      public StudentController(ILogger<StudentController> logger)
      {
          _logger = logger;
      }

      [HttpGet("get/{id}")]
      public async Task<StudentDTO> GetStudent(int id)
      {
          return Transformer.getStudentById(id);
      }
  }
}