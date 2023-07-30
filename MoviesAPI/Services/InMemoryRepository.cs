using System;
using System.Xml.Linq;
using MoviesAPI.Entities;

namespace MoviesAPI.Services {
  public class InMemoryRepository : IRepository {
    private List<Genre> _genres;

    public InMemoryRepository() {
      _genres = new List<Genre>() {
        new Genre() { Id = 1, Name = "Comedy" },
        new Genre() { Id = 2, Name = "Action" }
      };
    }

    // When using async keyword, return type has to be wrapped in Task<T>.
    public async Task<List<Genre>> GetAllGenres() {
      await Task.Delay(1);
      return _genres;
    }

    public async Task<Genre> GetGenreById(int Id) {
      return _genres.FirstOrDefault(x => x.Id == Id);
    }
  }
}