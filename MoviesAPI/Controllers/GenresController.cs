using System.Diagnostics.CodeAnalysis;
using MoviesAPI.Services;
using MoviesAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MoviesAPI.Controllers {
  // base route
  [Route("api/genres")] // alternately, can use [Route("api/[Controller]")], however this will break clients if we change the class name.
  [ApiController]
  [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
  public class GenresController : ControllerBase {
    private readonly ILogger<GenresController> logger;
    private readonly IRepository repository;

    public GenresController(ILogger<GenresController> logger, IRepository repository) {
      this.logger = logger;
      this.repository = repository;
    }

    // Can use multiple routes
    // [HttpGet] // api/genres
    [HttpGet("all")] // api/genres/all
    // Can override the base route
    // [HttpGet("/all-genres")] // allgenres
    public async Task<ActionResult<List<Genre>>> Get() {
      logger.LogDebug("Return all genres");
      return await repository.GetAllGenres();
    }

    [HttpGet("{id:int}", Name = "getGenre")] // With type constraint, will "404 Not Found" if wrong type. E.g. api/genres/1
    // [HttpGet("{id}")] // With no type constraint. Will "400 Bad Request" if can't find value (thus also if wrong type).
    // [HttpGet("{id}/{param2}")] // With 2 required parameters. E.g. api/genres/2/hi
    // [HttpGet("{id}/{param2=test}")] // With a default value. E.g. api/genres/1 or api/genres/2/hi
    public ActionResult<Genre> Get(int id){//, string param2) {
      var genre = repository.GetGenreById(id);
      if (genre == null) {
        logger.LogWarning($"Genre with Id {id} not found");
        return NotFound();
      }
      logger.LogDebug($"Return genre: id = {genre.Id}, name = {genre.Name}");
      return genre;
    }
  
    [HttpPost]
    public ActionResult Post([FromBody] Genre genre) {
      repository.AddGenre(genre);
      return Ok();
    }

    [HttpPut]
    public ActionResult Put() {
      return NoContent();
    }

    [HttpDelete]
    public ActionResult Delete() {
      return NoContent();
    }
  }
}

