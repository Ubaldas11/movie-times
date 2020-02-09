using Autofac;
using MovieTimes.Scraper.TypeRegistration;
using System.Threading.Tasks;

namespace MovieTimes.Scraper
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var container = ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var movieTimesCollector = scope.Resolve<MovieTimeCollector>();
                await movieTimesCollector.CollectMovieTimes();
            }
        }
    }
}
