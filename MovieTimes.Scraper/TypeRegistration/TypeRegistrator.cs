using Autofac;
using MovieTimes.Db;
using MovieTimes.Scraper.Scraper;
using System.Net.Http;

namespace MovieTimes.Scraper.TypeRegistration
{
    public class TypeRegistrator
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<HttpClient>().SingleInstance();
            builder.RegisterType<ForumCinemasScraper>().As<ICinemaScraper>();
            builder.RegisterType<MovieTimeCollector>();
            builder.RegisterType<MovieTimesContext>();
        }
    }
}
