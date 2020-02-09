using HtmlAgilityPack;
using MovieTimes.Scraper.Models;
using MovieTimes.Utils;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieTimes.Scraper.Scraper
{
    public class ForumCinemasScraper : ICinemaScraper
    {
        private readonly HttpClient _httpClient;
        public ForumCinemasScraper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ScrapedMovie>> ScrapeMovieShowings()
        {
            var forumCinemasUrl = "https://www.forumcinemas.lt/Movies/Vilnius";

            var response = await _httpClient.GetAsync(forumCinemasUrl);
            var responseContent = await response.Content.ReadAsStringAsync();

            var page = new HtmlDocument();
            page.LoadHtml(responseContent);

            var scrapedMovies = new List<ScrapedMovie>();

            var movieNodes = page.DocumentNode.SelectNodes("//td[contains(@class, 'result')]");
            foreach (var movieNode in movieNodes)
            {
                if (string.IsNullOrWhiteSpace(movieNode.InnerHtml))
                    continue;
                var movieName = movieNode.SelectSingleNode(".//a[contains(@class, 'result_h4')]").InnerText.Trim();
                var scrapedMovie = new ScrapedMovie()
                {
                    //remove brackets about dubbing
                    NormalizedMovieName = RemoveBracketsFromNameEnd(movieName).LowercaseAndRemoveWhitespace()
                };
                var cinemaBlocks = movieNode.SelectNodes(".//div[contains(@id, 'showTimes')]/*");
                foreach (var cinemaBlock in cinemaBlocks)
                {
                    var cinemaName = cinemaBlock.SelectSingleNode(".//div[contains(@style, 'clear: left')]").InnerText.Trim();
                    var scrapedMovieShowing = new ScrapedMovieShowing()
                    {
                        //remove city name
                        NormalizedCinemaNameWithoutCity = RemoveBracketsFromNameEnd(cinemaName).LowercaseAndRemoveWhitespace()
                    };
                    var cinemaTimes = cinemaBlock.SelectNodes(".//li");
                    foreach (var cinemaTime in cinemaTimes)
                    {
                        scrapedMovieShowing.StartTimes.Add(cinemaTime.InnerText.Trim());
                    }
                    scrapedMovie.MovieShowings.Add(scrapedMovieShowing);
                }
                scrapedMovies.Add(scrapedMovie);
            }
            return scrapedMovies;
        }

        private string RemoveBracketsFromNameEnd(string movieName)
        {
            var bracketIndex = movieName.IndexOf('(');
            return bracketIndex < 0 ?
                movieName :
                movieName.Substring(0, bracketIndex).Trim();
        }
    }
}
