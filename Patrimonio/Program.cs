using Patrimonio.Configurations;
using Patrimonio.Repositories;
using Patrimonio.Repositories.Implementations;
using Patrimonio.Services;
using Patrimonio.Services.Implementations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddSerilogLogging();

builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddGlobalExceptionHandling();
builder.Services.AddProblemDetails();
builder.Services.AddOpenAPIConfig();
builder.Services.AddSwaggerConfig();
builder.Services.AddRouteConfig();

builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<IAuditoriaRepository, AuditoriaRepository>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwaggerSpecification();

app.UseSerilogRequestLogging();

try
{
    app.Run();
}
finally
{
    //Estamos garantindo que o Serilog tente descarregar os eventos pendentes durante o encerramento normal da aplicação.
    Log.CloseAndFlush();
}

