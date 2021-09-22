using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MiniProject1.WebApi.Controllers
{
    [ApiController]
    [Route("/students")]
    public class StudentController : ControllerBase
    {
        //bare for at kunne få et output
        private static readonly string[] names = new[]
        {
            "Asger", "Vikke", "Martin", "Willi", "Svense"
        };

        private readonly ILogger<StudentController> _logger;
        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }

        //test metode
        [HttpGet("all")]
        public string GetStudents()
        {
            var rng = new Random();
            int randomPerson = rng.Next(0,4);
            return names[randomPerson];
        }

    }
}