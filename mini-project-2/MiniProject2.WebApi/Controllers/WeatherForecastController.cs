using Microsoft.AspNetCore.Mvc;
using MiniProject2.Factory.Clients;
using MiniProject2.Factory.DTO;

namespace MiniProject2.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<StudentDTO> Get()
    {
        return await StudentClient.GetStudentByIdAsync(1);
    }
}
