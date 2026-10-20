namespace Patrimonio.Configurations;

public static class RouteConfig
{
    public static IServiceCollection AddRouteConfig(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseQueryStrings = true;
            options.LowercaseUrls = true;
        });

        return services;
    }
}
