using MovieService.Enums;
using MovieService.Repositories;

namespace MovieService.Factories
{
    public class MovieRepositoryFactory : IMovieRepositoryFactory
    {
        private IEnumerable<IMovieRepository> _movieRepository;
        public MovieRepositoryFactory(IEnumerable<IMovieRepository> movieRepositories)
        {
            _movieRepository = movieRepositories;
        }
        public IMovieRepository Create(Source source)
        {
            switch (source)
            {
                case Source.Csv: return _movieRepository.First(x => x is CsvMovieRepository);
                case Source.Sqlite: return _movieRepository.First(x => x is SqliteMovieRepository);
                default: return _movieRepository.First(x => x is CsvMovieRepository);
            }
        }
    }
}