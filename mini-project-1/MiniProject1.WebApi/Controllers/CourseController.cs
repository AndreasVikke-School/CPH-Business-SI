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
    [Route("/course")]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;

        public CourseController(ILogger<CourseController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get/{id}")]
        public async Task<Course> GetCourseById(int id)
        {
            return await GrpcClient.GetCourse(id);
        }

        [HttpPost("add")]
        public async Task<Course> AddCourse(Course course)
        {
            return await GrpcClient.AddCourse(course);
        }

    }
}
