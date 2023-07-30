using System;
using MoviesAPI.Services;
using MoviesAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MoviesAPI.Controllers {
  [ApiController]
  [Route("api/genres")] // alternately, can use [Route("api/[Controller]")], however this will break clients if we change the class name.
  public class GenresController : ControllerBase {
    private readonly ILogger<GenresController> _logger;
    private readonly IRepository repository;

    public GenresController(ILogger<GenresController> logger, IRepository repository) {
      _logger = logger;
      this.repository = repository;
    }

    [HttpGet]
    public List<Genre> Get() {
      return repository.GetAllGenres();
    }

    [HttpGet]
    public Genre Get(int Id) {
      var genre = repository.GetGenreById(Id);
      if (genre == null) {
        //return NotFound();
      }
      return genre;
    }

    [HttpPost]
    public void Post() {
    }

    [HttpPut]
    public void Put() {
    }

    [HttpDelete]
    public void Delete() {
    }
  }
}

