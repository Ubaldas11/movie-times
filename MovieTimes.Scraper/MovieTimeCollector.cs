using MovieTimes.Db;
using MovieTimes.Db.Entities;
using MovieTimes.Utils;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTimes.Scraper
{
    public class MovieTimeCollector
    {
        private readonly ICinemaScraper _scraper;
        private readonly MovieTimesContext _context;
        public MovieTimeCollector(ICinemaScraper scraper, MovieTimesContext movieTimesContext)
        {
            _scraper = scraper;
            _context = movieTimesContext;
        }

        public async Task CollectMovieTimes()
        {
            //change to get a week span
            var dbMovies = _context.Movies.ToList();
            var scrapedMovies = await _scraper.ScrapeMovieShowings();

            scrapedMovies.ForEach((scrapedMovie) =>
            {
                dbMovies.ForEach((dbMovie) =>
                {
                    var dbMovieName = dbMovie.MovieName.LowercaseAndRemoveWhitespace();
                    var distance = StringUtils.CalculateLevenhsteinDistance(dbMovieName, scrapedMovie.NormalizedMovieName);
                    if (distance < 3)
                    {
                        scrapedMovie.MovieShowings.ForEach((scrapedMovieShowing) =>
                        {
                            var cinema = _context.Cinemas.ToList()
                                .Find(x => x.CinemaName.LowercaseAndRemoveWhitespace() == scrapedMovieShowing.NormalizedCinemaNameWithoutCity);
                            var movieShowings = scrapedMovieShowing.StartTimes.Select((scrapedTime) => new MovieShowing()
                            {
                                CinemaId = cinema.CinemaId,
                                MovieId = dbMovie.MovieId,
                                StartTime = DateTime.ParseExact(scrapedTime, "HH:mm", CultureInfo.CurrentCulture)
                            });
                            _context.MovieShowings.AddRange(movieShowings);
                        });
                        
                    }
                });
            });
            _context.SaveChanges();
        }
    }
}
