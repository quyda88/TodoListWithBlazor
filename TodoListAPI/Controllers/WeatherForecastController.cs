using Microsoft.AspNetCore.Mvc;
using TodoListAPI.Repositories;
using TodoListAPI.Entities;

namespace TodoListAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMyDependency _myDependency;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMyDependency myDependency)
        {
            _logger = logger;
            _myDependency = myDependency;
        }

        //[HttpGet(Name = "TestDI")]
        //public void OnGet()
        //{

        //}
        public string Message { get; set; } = string.Empty;
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _myDependency.WriteMessage("Index2Model.OnGet");
            Message = $"About page visited at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
