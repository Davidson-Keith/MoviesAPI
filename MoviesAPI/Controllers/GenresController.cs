using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using MoviesAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Expressions;
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
      logger.LogDebug("HttpGet all : Return all genres");

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
      var genre = await dbContext.Genres.FindAsync(id);
      return mapper.Map<ActionResult<Genre>>(genre);
    }

    // --------------
    // Some playing around with Get methods
    // --------------

    [HttpGet("byName")] // E.g. api/genres/byName?name=Drama
    // [HttpGet("byName/{name}")] // E.g. api/genres/byName/Drama - This type of overloading doesn't work
    public async Task<ActionResult<List<GenreDto>>> GetByName(string name) {
      // Return as a single. This works, but requires a different method return type - Task<ActionResult<Genre>>
      // var genre = await dbContext.Genres.FirstAsync(genre => genre.Name.Equals(name)); 
      // return mapper.Map<Genre>(genre);

      // Return as a list. Since we haven't defined the Name column to be unique, this makes more sense.
      // Of course, we should make the Name column should be unique.
      var genres = await dbContext.Genres.Where(genre => genre.Name.Equals(name)).ToListAsync();
      return mapper.Map<List<GenreDto>>(genres);
    }

    [HttpGet("test")] // api/genres/test
    public ActionResult GetTest() {
      return Ok("Test ok");
    }

    // This WON'T work for api/genres/test?message=hi-there, as it will send it to the above method and simply
    // ignore the parameter
    [HttpGet("test/{message}")] // E.g. api/genres/test/hi-there
    public ActionResult<string> Get(string message) {
      // if (message.IsNullOrEmpty()) {
      //   return Ok("Message null or empty"); //This never gets here. With GetTest() removed, returns 404. With GetTest(), it routes to that instead.
      // }
      return Ok($"Message = {message}"); // The Ok is redundant, but makes it clearer I suppose.
      // return $"Message = {message}"; // This works too, also with the header 200 OK.
    }

    // NB: to use the params in the ?id=3... format, then DON'T put the params in the template. 
    [HttpGet("multi")] // E.g. api/genres/multi?id=3&message=hi-there
    public ActionResult<string> Get(int id, string message) {
      return Ok($"ID = {id}, Message = {message}");
    }
    // --------------
    // End playing around
    // --------------


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