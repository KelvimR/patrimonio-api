using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace Patrimonio.Configurations;

public static class LoggingConfig
{
    public static void AddSerilogLogging(this WebApplicationBuilder builder)
    {
        SelfLog.Enable(message =>
        {
            Console.Error.WriteLine(message);
        });
        
        var logDirectory = Path.Combine(
           builder.Environment.ContentRootPath,
           "logs");

        Directory.CreateDirectory(logDirectory);

        var logPath = Path.Combine(logDirectory, "patrimonio-.json");

        Console.WriteLine($"Destino dos logs: {logPath}");

        builder.Services.AddSerilog((services, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(builder.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "Patrimonio.Api") // Identificar a aplicação que gerou o log
                .WriteTo.Console()
                .WriteTo.File(
                    new CompactJsonFormatter(),
                    logPath,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30, // Manter até 30 arquivos de log
                    fileSizeLimitBytes: 10_485_760, //Limitar cada arquivo a aproximadamente 10 MB
                    rollOnFileSizeLimit: true);
        });
    }
}
        
