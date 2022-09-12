using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MovieService.Controllers;
using MovieService.Data;
using MovieService.Factories;
using MovieService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Test
{
    [TestFixture]
    public class MoviesControllerTest
    {
        private MoviesController _moviesController;

        [OneTimeSetUp]
        public void Setup()
        {
            _moviesController = new MoviesController(new Mock<ILogger<MoviesController>>().Object,
                                                        new MovieRepositoryFactory(new List<IMovieRepository> { new CsvMovieRepository(new CsvContext()), new SqliteMovieRepository(new SqliteDbContext()) }));
        }

        [Test]
        public void MoviesController_Csv_Movie()
        {
            var movie = new Movie
            {
                Id = 12,
                Title = "89",
                Year = 2017,
                Director = "\"Dave Stewart\""
            };

            var response = _moviesController.GetById(12, Enums.Source.Csv) as OkObjectResult;
            var movieResponse = response.Value as Movie;

            Assert.That(movieResponse.Id, Is.EqualTo(movie.Id));
            Assert.That(movieResponse.Title, Is.EqualTo(movie.Title));
            Assert.That(movieResponse.Year, Is.EqualTo(movie.Year));
            Assert.That(movieResponse.Director, Is.EqualTo(movie.Director));
        }

        [Test]
        public void MoviesController_Sqlite_Movie()
        {
            var movie = new Movie
            {
                Id = 12,
                Title = "\"Secrets in the Fall\"",
                Year = 2015,
                Director = "\"Brittany Goodwin\""
            };

            var response = _moviesController.GetById(12, Enums.Source.Sqlite) as OkObjectResult;
            var movieResponse = response.Value as Movie;

            Assert.That(movieResponse.Id, Is.EqualTo(movie.Id));
            Assert.That(movieResponse.Title, Is.EqualTo(movie.Title));
            Assert.That(movieResponse.Year, Is.EqualTo(movie.Year));
            Assert.That(movieResponse.Director, Is.EqualTo(movie.Director));
        }
    }
}
