namespace Api.Services.Core.SmartCharging.Infrastructure
{
    public static class Bootstrapper
    {
        public static IServiceProvider Initialize(this IServiceCollection serviceCollection, IConfiguration configuration)
        {

            return serviceCollection.BuildServiceProvider();
        }
    }
}
