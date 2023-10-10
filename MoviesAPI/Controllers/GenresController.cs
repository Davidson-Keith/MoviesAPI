using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using MoviesAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MoviesAPI.Controllers {
  // base route
  [Route("api/genres")] // alternately, can use [Route("api/[Controller]")], however this will break clients if we change the class name.
  [ApiController]
  [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
  public class GenresController : ControllerBase {
    private readonly ILogger<GenresController> logger;

    public GenresController(ILogger<GenresController> logger) {
      this.logger = logger;
    }

    [HttpGet] // api/genres
    [HttpGet("all")] // api/genres/all
    public async Task<ActionResult<List<Genre>>> Get() {
      logger.LogDebug("Return all genres");
      // Temp return data for testing.
      return new List<Genre>() {  
        new Genre() { Id = 1, Name = "Comedy" }, 
        new Genre(){Id = 2, Name = "Action"}
      };
    }

    [HttpGet("{id:int}", Name = "getGenre")] // E.g. api/genres/1
    public ActionResult<Genre> Get(int id) {
      throw new NotImplementedException();
    }
  
    [HttpPost]
    public ActionResult Post([FromBody] Genre genre) {
      throw new NotImplementedException();
    }

    [HttpPut]
    public ActionResult Put() {
      throw new NotImplementedException();
    }

    [HttpDelete]
    public ActionResult Delete() {
      throw new NotImplementedException();
    }
  }
}

