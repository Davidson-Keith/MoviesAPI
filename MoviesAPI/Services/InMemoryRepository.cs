using System;
using System.Xml.Linq;
using MoviesAPI.Entities;

namespace MoviesAPI.Services {
  public class InMemoryRepository : IRepository {
    private List<Genre> genres;

    public InMemoryRepository() {
      genres = new List<Genre>() {
        new Genre() { Id = 1, Name = "Comedy" },
        new Genre() { Id = 2, Name = "Action" }
      };
    }

    // When using async keyword, return type has to be wrapped in Task<T>.
    public async Task<List<Genre>> GetAllGenres() {
      await Task.Delay(1);
      return genres;
    }

    public Genre GetGenreById(int Id) {
      // await Task.Delay(1);
      return genres.FirstOrDefault(x => x.Id == Id);
    }

    public void AddGenre(Genre genre) {
      genre.Id = genres.Max(x => x.Id) + 1;
      genres.Add(genre);
    }
  }
}