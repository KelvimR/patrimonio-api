using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi;

namespace Patrimonio.Configurations;

public static class OpenAPIConfig
{
    private static readonly string AppName = "API REST Patrimonio";
    private static readonly string AppDescription = "API REST do Controle Patrimonial de Inventário";

    public static IServiceCollection AddOpenAPIConfig(this IServiceCollection services)
    {
        services.AddSingleton(new OpenApiInfo
        {
            Title = AppName,
            Version = "v1",
            Description = AppDescription,
            Contact = new OpenApiContact
            {
                Name = "Kelvim Rodrigues dos Santos",
                Email = "Kelvimrodrigues1@gmail.com",
                Url = new Uri("https://www.linkedin.com/in/kelvim-rodrigues-dev")
            },
            License = new OpenApiLicense
            {
                Name = "MIT License",
                Url = new Uri("https://opensource.org/licenses/MIT")
            }
        });

        return services;
    }

}
