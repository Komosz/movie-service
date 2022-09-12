using MovieService.Data;

namespace MovieService.Repositories
{
    public interface IMovieRepository
    {
        public IEnumerable<Movie> ReadAll();
        public IEnumerable<Movie> Read(string title, int year, string director);
        public Movie ReadById(int id);
    }
}
