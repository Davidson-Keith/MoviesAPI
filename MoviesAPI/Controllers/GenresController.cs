using System;
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
    public ActionResult<List<Genre>> Get() {
      return repository.GetAllGenres();
    }

    [HttpGet("{id:int}")] // With type constraint, will "404 Not Found" if wrong type e.g. api/genres/1
    // [HttpGet("{id}")] // with no type constraint. Will "400 Bad Request" if wrong type.
    // [HttpGet("{id}/{param2}")] // with 2 required parameters, e.g. api/genres/2/hi
    // [HttpGet("{id}/{param2=test}")] // with a default value e.g. api/genres/1 or api/genres/2/hi
    public ActionResult<Genre> Get(int id) {
      var genre = repository.GetGenreById(id);
      if (genre == null) {
        return NotFound();
      }
      return genre;
    }

    [HttpPost]
    public ActionResult Post() {
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

