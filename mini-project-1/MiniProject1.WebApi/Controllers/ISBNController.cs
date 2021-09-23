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
    [Route("/ISBNValidator")]
    public class ISBNController : ControllerBase
    {
        private readonly ILogger<ISBNController> _logger;
        public ISBNController(ILogger<ISBNController> logger)
        {
            _logger = logger;
        }

        [HttpGet("10/{isbn}")]
        public async Task<bool> GetStudent(int isbn)
        {
            return await SoapConnector.ISBN10Validator(isbn);
        }

        [HttpGet("13/{isbn}")]
        public async Task<bool> GetStudent(int isbn)
        {
            return await SoapConnector.ISBN13Validator(isbn);
        }
    }
}