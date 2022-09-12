using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MovieService.Data
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public SqliteDbContext() => SqliteInitialize();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName=sqlitedb", option =>
            {
                option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().ToTable("Movies", "test");
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.HasIndex(i => i.Title);
            });
            base.OnModelCreating(modelBuilder);
        }

        private void SqliteInitialize()
        {
            var dbName = "sqlitedb";
            if (File.Exists(dbName))
                return;
                //File.Delete(dbName);

            Database.EnsureCreated();

            var path = Path.Combine(Environment.CurrentDirectory, "Data", "Hydra-Movie-Scrape-Sqlite.csv");

            Movies.AddRange(
                File.ReadLines(path)
                .Skip(1)
                .Select(ParseMovieFromStrongRow).ToList());
            SaveChanges();
        }

        private Movie ParseMovieFromStrongRow(string row)
        {
            //string[] column = row.Split(',');
            string[] column = Regex.Split(row, @",(?=(?:[^""]*""[^""]*"")*[^""]*$)");

            return new Movie
            {
                Title = column[0],
                Year = int.Parse(column[1]),
                Summary = column[2],
                ShortSummary = column[3],
                IMDBID = column[4],
                Runtime = int.Parse(column[5]),
                YouTubeTrailer = column[6],
                Rating = float.Parse(column[7].Replace('.', ',')),
                MoviePoster = column[8],
                Director = column[9],
                Writers = column[10],
                Cast = column[11]
            };
        }
    }
}
