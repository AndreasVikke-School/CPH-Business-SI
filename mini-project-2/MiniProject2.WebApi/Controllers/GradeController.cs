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

        [HttpGet("all")]
        public async Task<List<GradeDTO>> GetAllGrades()
        {
            return await GradeFactory.getAllGrades();
        }

        [HttpGet("get/{id}")]
        public async Task<GradeDTO> GetGrade(long id)
        {
            return await GradeFactory.getGradeById(id);
        }


        [HttpPost("add")]
        public async Task<GradeDTO> AddGradeToStudent(AddGradeDTO grade)
        {
            return await GradeFactory.AddGradeToStudent(grade);
        }

        [HttpGet("passed")]
        public async Task<List<GradeDTO>> getPassedStudents()
        {
            return await GradeFactory.getPassedStudents();
        }

        [HttpGet("failed")]
        public async Task<long> GetFailedStudents()
        {
            return await GradeFactory.getFailedStudents();
        }

    }
}