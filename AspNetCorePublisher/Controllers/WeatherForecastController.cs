using Microsoft.AspNetCore.Mvc;
using Share;
using System;

namespace DockerDemoWebApi.Controllers
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
        private readonly RabbitMQPublisher _rabbitMQPublisher;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, RabbitMQPublisher rabbitMQPublisher)
        {
            _logger = logger;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        [HttpPost]
        public IActionResult Post()
        {
            var weatherForecast = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };

            _rabbitMQPublisher.Publish(weatherForecast);

            return Ok(weatherForecast);
        }
    }
}
