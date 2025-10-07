using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Exceptions;

namespace Shared.Logging;

public static class LoggingConfiguration
{
    public static void ConfigureSerilog(this ConfigureHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration)
                .Enrich.WithExceptionDetails()
                .Enrich.FromLogContext();
        });
    }
}