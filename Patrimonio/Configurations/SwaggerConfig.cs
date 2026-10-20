using Microsoft.OpenApi;

namespace Patrimonio.Configurations
{
    public static class SwaggerConfig
    {
        private static readonly string AppName = "API REST Patrimonio";
        private static readonly string AppDescription = "API REST do Controle Patrimonial de Inventário";

        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = AppName,
                    Version = "v1",
                    Description = AppDescription,
                    Contact = new OpenApiContact
                    {
                        Name = "Kelvim Rodrigues",
                        Email = "kelvimrodrigues1@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/kelvim-rodrigues-dev")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                // Evita conflitos de nomes de classes na documentação do Swagger, usando o nome completo da classe (incluindo namespace) como identificador único.
                options.CustomSchemaIds(type => type.FullName);

            });

            return services;
        }
        
        public static IApplicationBuilder UseSwaggerSpecification(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = "swagger-ui"; 
                options.DocumentTitle = AppName;
            });

            return app;
        }
    }
}
