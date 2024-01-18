using NUnit.Framework;
using MoviesAPI.Controllers;
using MoviesAPI.Entities;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MoviesAPI.Tests {
  [TestFixture]
  public class GenresControllerTests {
    private GenresController _controller;

    [SetUp]
    public void Setup() {
      var logger = new LoggerFactory().CreateLogger<GenresController>();
      _controller = new GenresController(logger);
    }

    [Test]
    public async Task Get_ReturnsListOfGenres() {
      // Arrange

      // Act
      var result = await _controller.Get();

      // Assert
      Assert.IsInstanceOf<ActionResult<List<Genre>>>(result);
      Assert.IsNotNull(result.Value);
      Assert.AreEqual(2, result.Value.Count);
    }

    [Test]
    public void Get_WithValidId_ReturnsGenre() {
      // Arrange
      int id = 1;

      // Act
      var result = _controller.Get(id);

      // Assert
      Assert.IsInstanceOf<ActionResult<Genre>>(result);
      Assert.IsNotNull(result.Value);
      Assert.AreEqual(id, result.Value.Id);
    }

    [Test]
    public void Get_WithInvalidId_ReturnsNotFound() {
      // Arrange
      int id = 10;

      // Act
      var result = _controller.Get(id);

      // Assert
      Assert.IsInstanceOf<ActionResult<Genre>>(result);
      Assert.IsNull(result.Value);
      Assert.IsInstanceOf<NotFoundResult>(result.Result);
    }

    [Test]
    [Ignore("Ignore a test")]
    public void Post_ReturnsNotImplemented() {
      // Arrange
      var genre = new Genre();

      // Act
      var result = _controller.Post(genre);

      // Assert
      // Assert.IsInstanceOf<NotImplementedResult>(result);
    }

    [Test]
    [Ignore("Ignore a test")]
    public void Put_ReturnsNotImplemented() {
      // Act
      var result = _controller.Put();

      // Assert
      // Assert.IsInstanceOf<NotImplementedResult>(result);
    }

    [Test]
    [Ignore("Ignore a test")]
    public void Delete_ReturnsNotImplemented() {
      // Act
      var result = _controller.Delete();

      // Assert
      // Assert.IsInstanceOf<NotImplementedResult>(result);
    }
  }
}