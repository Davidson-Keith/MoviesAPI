using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("[controller]")] // Translates to "Weatherforecast", taken from "WeatherforecastController"
public class WeatherForecastController : ControllerBase {
  private static readonly string[] Summaries = new[] {
        "1Freezing", "1Bracing", "1Chilly", "1Cool"
  };

  private readonly ILogger<WeatherForecastController> _logger;

  public WeatherForecastController(ILogger<WeatherForecastController> logger) { //}, IRepository repository) {
    _logger = logger;
  }

  // Just the base route. "Name" isn't a URL, it is a name. What is it for? Inserting a named value?
  [HttpGet(Name = "GetWeatherForecast")]  // WeatherForecast (http://localhost:5233/WeatherForecast)
  // Prepend the base route
  // [HttpGet("GetWeatherForecast")] // Weatherforecast/GetWeatherforecast 
  // Override the base route
  // [HttpGet("/GetWeatherForecast")] // GetWeatherForecast 
  public IEnumerable<WeatherForecast> Get() {
    return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
      Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
      TemperatureC = Random.Shared.Next(-20, 55),
      Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    })
    .ToArray();
  }
}

