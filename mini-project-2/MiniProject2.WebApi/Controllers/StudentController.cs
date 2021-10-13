using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using MiniProject2.Factory;
using MiniProject2.Models.DTO;
using MiniProject2.Models.Models;

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
          return await Transformer.getStudentById(id);
      }

      [HttpGet("all")]
      public async Task<List<StudentDTO>> GetStudents()
      {
          return await Transformer.getStudents();
      }
      [HttpPost("add")]
      public async Task<StudentDTO> AddStudent(AddStudentDTO student)
      {
          return await Transformer.AddStudent(student);
      }

  }
}