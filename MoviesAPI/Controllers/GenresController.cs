using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using MoviesAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;

namespace MoviesAPI.Controllers {
  // base route
  [Route("api/genres")]
  // [Route("api/genres/{genreCreationDTO}")] // alternately, can use [Route("api/[Controller]")], however this will break clients if we change the class name.
  [ApiController]
  [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
  public class GenresController : ControllerBase {
    private readonly ILogger<GenresController> logger;
    private readonly ApplicationDbContext dbContext;
    private readonly IMapper mapper;

    public GenresController(ILogger<GenresController> logger, ApplicationDbContext dbContext, IMapper mapper) {
      this.logger = logger;
      this.dbContext = dbContext;
      this.mapper = mapper;
    }

    [HttpGet] // api/genres
    [HttpGet("all")] // api/genres/all
    public async Task<ActionResult<List<GenreDto>>> Get() {
      logger.LogDebug("Return all genres");

      // Return the GenresDTO from the DB as a list
      var genres = await dbContext.Genres.ToListAsync();
      return mapper.Map<List<GenreDto>>(genres);

      // Temp return data for testing.
      // return new List<Genre>() {  
      //   new Genre() { Id = 1, Name = "Comedy" }, 
      //   new Genre(){Id = 2, Name = "Action"}
      // };
    }

    [HttpGet("{id:int}", Name = "getGenre")] // E.g. api/genres/1
    public async Task<ActionResult<Genre>> Get(int id) {
      // Return the GenreDTO with the given ID
      var genre = dbContext.Genres.Find(id);
      return mapper.Map<ActionResult<Genre>>(genre);

      // throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] GenreCreationDto genreCreationDto) {
    // public async Task<ActionResult<List<GenreDto>>> Post([FromBody] GenreCreationDto genreCreationDto) {
      // Write new genreCreationDto object to DB. DB creates the Id.
      dbContext.Genres.Add(mapper.Map<Genre>(genreCreationDto));
      await dbContext.SaveChangesAsync();
      // return await Get();
      return NoContent();
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