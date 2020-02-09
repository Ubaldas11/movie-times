using System.Collections.Generic;

namespace MovieTimes.Scraper.Models
{
    public class ScrapedMovie
    {
        public string NormalizedMovieName { get; set; }
        public List<ScrapedMovieShowing> MovieShowings { get; set; } = new List<ScrapedMovieShowing>();
    }

    public class ScrapedMovieShowing
    {
        public string NormalizedCinemaNameWithoutCity { get; set; }
        public List<string> StartTimes { get; set; } = new List<string>();
    }

}
