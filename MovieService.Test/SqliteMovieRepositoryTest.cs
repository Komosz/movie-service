using MovieService.Data;
using MovieService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Test
{
    [TestFixture]
    public class SqliteMovieRepositoryTest
    {
        private SqliteMovieRepository _sqliteMovieRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            _sqliteMovieRepository = new SqliteMovieRepository(new SqliteDbContext());
        }

        [Test]
        public void SqliteMovieRepository_CorrectId_Movie()
        {
            var movie = new Movie
            {
                Id = 2,
                Title = "\"Kung Fu Panda 2\"",
                Year = 2011,
                Director = "\"Jennifer Yuh Nelson\""
            };

            var response = _sqliteMovieRepository.ReadById(2);

            Assert.That(response.Id, Is.EqualTo(movie.Id));
            Assert.That(response.Title, Is.EqualTo(movie.Title));
            Assert.That(response.Year, Is.EqualTo(movie.Year));
            Assert.That(response.Director, Is.EqualTo(movie.Director));
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(int.MaxValue)]
        public void SqliteMovieRepository_OutOfRangeId_InvalidOperationException(int id)
        {
            Assert.That(_sqliteMovieRepository.ReadById(id), Is.EqualTo(null));
            //Assert.Throws<InvalidOperationException>(() => _sqliteMovieRepository.ReadById(id));
        }

        [Test]
        public void SqliteMovieRepository_CorrectQuery_Movie()
        {
            var movie = new Movie
            {
                Id = 3,
                Title = "\"Kung Fu Panda 3\"",
                Year = 2016,
                Director = "\"Alessandro Carloni\""
            };

            var response = _sqliteMovieRepository.Read("Kung Fu Panda", 2016, null).Single();

            Assert.That(response.Id, Is.EqualTo(movie.Id));
            Assert.That(response.Title, Is.EqualTo(movie.Title));
            Assert.That(response.Year, Is.EqualTo(movie.Year));
            Assert.That(response.Director, Is.EqualTo(movie.Director));
        }
    }
}
