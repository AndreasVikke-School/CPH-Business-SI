using System;
using MiniProject2.Models.DTO;
using MiniProject2.Factory.Factory;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using MiniProject2.EF.DatabaseContexts;


namespace MiniProject2.WebApi.Controllers
{
    [ApiController]
    [Route("/grade")]
    public class GradeController : ControllerBase
    {
        private readonly ILogger<GradeController> _logger;
        public GradeController(ILogger<GradeController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("get/{id}")]
        public async Task<GradeDTO> GetGrade(long id)
        {
            return await GradeFactory.getGradeById(id);
        }

        [HttpGet("all")]
        public async Task<List<GradeDTO>> GetAllGrades()
        {
            return await GradeFactory.getAllGrades();
        }

        [HttpPost("add")]
        public async Task<GradeDTO> AddGradeToStudent(AddGradeDTO grade)
        {
            return await GradeFactory.AddGradeToStudent(grade);
        }

        [HttpGet("passed/{examId}")]
        public async Task<List<GradeDTO>> getPassedStudents(int examId)
        {
            return await GradeFactory.getPassedStudents(examId);
        }

        [HttpGet("failed/{examId}")]
        public async Task<long> GetFailedStudents(int examId)
        {
            return await GradeFactory.getFailedStudents(examId);
        }

    }
}