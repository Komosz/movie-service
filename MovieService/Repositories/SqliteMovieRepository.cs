using MovieService.Data;

namespace MovieService.Repositories
{
    public class SqliteMovieRepository : IMovieRepository
    {
        private SqliteDbContext _context;
        public SqliteMovieRepository(SqliteDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Movie> ReadAll()
        {
            return _context.Movies.Take(10);
        }
        public IEnumerable<Movie> Read(string title, int year, string director)
        {
            return _context.Movies.Where(m => m.Title.Contains(title ?? m.Title) && m.Year == (year > 0 ? year : m.Year) && m.Director.Contains(director ?? m.Director)).Take(10);
        }

        public Movie ReadById(int id)
        {
            return _context.Movies.SingleOrDefault(m => m.Id == id);
        }
    }
}