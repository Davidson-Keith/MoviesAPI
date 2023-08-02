using System;
using MoviesAPI.Entities;

namespace MoviesAPI.Services {
  public interface IRepository {
    Task<List<Genre>> GetAllGenres();
    Task<Genre> GetGenreById(int Id);
  }
}

