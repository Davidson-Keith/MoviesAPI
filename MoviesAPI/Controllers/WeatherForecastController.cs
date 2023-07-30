using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase {
  private static readonly string[] Summaries = new[] {
        "1Freezing", "1Bracing", "1Chilly", "1Cool"
  };

  private readonly ILogger<WeatherForecastController> _logger;

  public WeatherForecastController(ILogger<WeatherForecastController> logger) { //}, IRepository repository) {
    _logger = logger;
  }

  [HttpGet(Name = "GetWeatherForecast")] // WTF does this do?! This URL doesn't work.
  public IEnumerable<WeatherForecast> Get() {
    return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
      Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
      TemperatureC = Random.Shared.Next(-20, 55),
      Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    })
    .ToArray();
  }
}

