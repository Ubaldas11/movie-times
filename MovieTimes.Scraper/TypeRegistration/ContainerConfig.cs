using Autofac;

namespace MovieTimes.Scraper.TypeRegistration
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            TypeRegistrator.RegisterTypes(builder);
            var appContainer = builder.Build();

            return appContainer;
        }
    }
}
