using MovieService.Enums;
using MovieService.Repositories;

namespace MovieService.Factories
{
    public interface IMovieRepositoryFactory
    {
        IMovieRepository Create(Source source);
    }
}
