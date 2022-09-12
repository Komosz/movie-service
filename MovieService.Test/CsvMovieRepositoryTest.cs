using MovieService.Controllers;
using MovieService.Data;
using MovieService.Repositories;

namespace MovieService.Test
{
    [TestFixture]
    public class CsvMovieRepositoryTest
    {
        private CsvMovieRepository _csvMovieRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            _csvMovieRepository = new CsvMovieRepository(new CsvContext());
        }

        [Test]
        public void CsvMovieRepository_CorrectId_Movie()
        {
            var movie = new Movie
            {
                Id = 1,
                Title = "\"Kung Fu Panda\"",
                Year = 2008,
                Director = "\"Mark Osborne\""
            };

            var response = _csvMovieRepository.ReadById(1);

            Assert.That(response.Id, Is.EqualTo(movie.Id));
            Assert.That(response.Title, Is.EqualTo(movie.Title));
            Assert.That(response.Year, Is.EqualTo(movie.Year));
            Assert.That(response.Director, Is.EqualTo(movie.Director));
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(int.MaxValue)]
        public void CsvMovieRepository_OutOfRangeId_InvalidOperationException(int id)
        {
            Assert.That(_csvMovieRepository.ReadById(id), Is.EqualTo(null));
            //Assert.Throws<InvalidOperationException>(() => _csvMovieRepository.ReadById(id));
        }

        [Test]
        public void CsvMovieRepository_CorrectQuery_Movie()
        {
            var movie = new Movie
            {
                Id = 3,
                Title = "\"Kung Fu Panda 3\"",
                Year = 2016,
                Director = "\"Alessandro Carloni\""
            };

            var response = _csvMovieRepository.Read("Kung Fu Panda", 2016, null).Single();


            Assert.That(response.Id, Is.EqualTo(movie.Id));
            Assert.That(response.Title, Is.EqualTo(movie.Title));
            Assert.That(response.Year, Is.EqualTo(movie.Year));
            Assert.That(response.Director, Is.EqualTo(movie.Director));
        }
    }
}