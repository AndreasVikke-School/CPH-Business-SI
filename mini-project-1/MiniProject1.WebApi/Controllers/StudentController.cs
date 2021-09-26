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
    [Route("/students")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get/{id}")]
        public async Task<Student> GetStudent(int id)
        {
            return await GrpcClient.Get(id);
        }

        [HttpGet("get/all")]
        public async Task<List<Student>> GetAllStudents()
        {
            return await GrpcClient.GetAll();
        }

        [HttpPost("add")]
        public async Task<Student> AddStudent(Student student)
        {
            return await GrpcClient.AddStudent(student); 
        }
    }
}