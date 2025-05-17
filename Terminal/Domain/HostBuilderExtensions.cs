using Microsoft.Extensions.Hosting;
using Spectre.Console.Cli;

namespace Terminal.Domain;

public static class HostBuilderExtensions
{
#region Extensions

    public static CommandApp BuildApp(
        this IHostBuilder builder
    ) {
        var scRegistrar = new SpectreTypeRegistrar(builder);
        var application = new CommandApp(scRegistrar);
        
        return application;
    }

#endregion
}
