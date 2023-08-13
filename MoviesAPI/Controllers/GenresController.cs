using MoviesAPI.Services;
using MoviesAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MoviesAPI.Controllers {
  // base route
  [Route("api/genres")] // alternately, can use [Route("api/[Controller]")], however this will break clients if we change the class name.
  [ApiController]
  public class GenresController : ControllerBase {
    private readonly ILogger<GenresController> _logger;
    private readonly IRepository repository;

    public GenresController(ILogger<GenresController> logger, IRepository repository) {
      _logger = logger;
      this.repository = repository;
    }

    // Can use multiple routes
    [HttpGet] // api/genres
    [HttpGet("all")] // api/genres/all
    // Can override the base route
    [HttpGet("/all-genres")] // allgenres
    public async Task<ActionResult<List<Genre>>> Get() {
      return await repository.GetAllGenres();
    }

    [HttpGet("{id:int}")] // With type constraint, will "404 Not Found" if wrong type. E.g. api/genres/1
    // [HttpGet("{id}")] // With no type constraint. Will "400 Bad Request" if can't find value (thus also if wrong type).
    // [HttpGet("{id}/{param2}")] // With 2 required parameters. E.g. api/genres/2/hi
    // [HttpGet("{id}/{param2=test}")] // With a default value. E.g. api/genres/1 or api/genres/2/hi
    public async Task<ActionResult<Genre>> Get(int id, string param2) {
      // This is not required because the [ApiController] attribute indicates to do all the validation automatically.
      // if (!ModelState.IsValid) {
      //   return BadRequest(ModelState);
      // }
      var genre = repository.GetGenreById(id);
      if (genre == null) {
        return NotFound();
      }
      return await genre;
    }

    [HttpPost]
    public ActionResult Post([FromBody] Genre genre) {
      return NoContent();
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

