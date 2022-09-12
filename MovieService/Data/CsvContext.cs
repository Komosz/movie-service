using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace MovieService.Data
{
    public class CsvContext
    {
        public ICollection<Movie> Movies { get; set; }

        public CsvContext()
        {
            Init();
        }

        public void Init()
        {
            int i = 1;

            var path = Path.Combine(Environment.CurrentDirectory, "Data", "Hydra-Movie-Scrape-Csv.csv");

            Movies = System.IO.File.ReadLines(path)
            .Skip(1)
            .Select(r => ParseMovieFromStrongRow(r, i++)).ToList();
        }
        private Movie ParseMovieFromStrongRow(string row, int i)
        {
            string[] column = Regex.Split(row, @",(?=(?:[^""]*""[^""]*"")*[^""]*$)");

            return new Movie
            {
                Id = i,
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
