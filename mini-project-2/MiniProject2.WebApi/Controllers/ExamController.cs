using Microsoft.AspNetCore.Mvc;
using MiniProject2.Factory;
using MiniProject2.Models.DTO;

namespace MiniProject2.WebApi.Controllers
{

  [ApiController]
  [Route("/exams")]
  public class ExamController : ControllerBase
  {
      private readonly ILogger<ExamController> _logger;
      public ExamController(ILogger<ExamController> logger)
      {
          _logger = logger;
      }

      [HttpGet("get/{id}")]
      public async Task<ExamDTO> GetExam(int id)
      {
          return await ExamFactory.getExamById(id);
      }

      [HttpGet("all")]
      public async Task<List<ExamDTO>> GetExams()
      {
          return await ExamFactory.getExams();
      }
      [HttpPost("add")]
      public async Task<ExamDTO> AddExam(AddExamDTO exam)
      {
          return await ExamFactory.AddExam(exam);
      }

  }
}