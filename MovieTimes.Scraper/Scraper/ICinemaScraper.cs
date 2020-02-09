using MovieTimes.Scraper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTimes.Scraper
{
    public interface ICinemaScraper
    {
        public Task<List<ScrapedMovie>> ScrapeMovieShowings();
    }
}
