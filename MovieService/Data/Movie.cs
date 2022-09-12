using System.ComponentModel.DataAnnotations;

namespace MovieService.Data
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public int Year { get; set; }
        [StringLength(1000)]
        public string? Summary { get; set; }
        [StringLength(1000)]
        public string? ShortSummary { get; set; }
        [StringLength(100)]
        public string? IMDBID { get; set; }
        public int Runtime { get; set; }
        [StringLength(100)]
        public string? YouTubeTrailer { get; set; }
        public float Rating { get; set; }
        [StringLength(100)]
        public string? MoviePoster { get; set; }
        [StringLength(100)]
        public string Director { get; set; }
        [StringLength(100)]
        public string? Writers { get; set; }
        [StringLength(100)]
        public string? Cast { get; set; }
    }
}
