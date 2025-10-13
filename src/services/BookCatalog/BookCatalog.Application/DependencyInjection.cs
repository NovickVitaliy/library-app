using Microsoft.Extensions.DependencyInjection;

namespace BookCatalog.Application;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        return services;
    }
}