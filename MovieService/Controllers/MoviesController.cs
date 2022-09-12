using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieService.Data;
using MovieService.Enums;
using MovieService.Factories;
using MovieService.Repositories;
using System.IO;

namespace MovieService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IMovieRepositoryFactory _movieRepositoryFactory;
        public MoviesController(ILogger<MoviesController> logger, IMovieRepositoryFactory movieRepositoryFactory)
        {
            _logger = logger;
            _movieRepositoryFactory = movieRepositoryFactory;
        }

        [HttpGet]
        public IEnumerable<Movie> GetAll(Source source)
        {
            var movieRepository = _movieRepositoryFactory.Create(source);

            return movieRepository.ReadAll();
        }

        [HttpGet]
        [Route("by-query")]
        public IEnumerable<Movie> Get(Source source, string? title, int year, string? director)
        {
            var movieRepository = _movieRepositoryFactory.Create(source);

            return movieRepository.Read(title, year, director);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id, Source source)
        {
            var movieRepository = _movieRepositoryFactory.Create(source);

            var response = movieRepository.ReadById(id);

            if (response == null)
            {
                _logger.LogError("Out of range ID", DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }

            return Ok(response);
        }
    }
}