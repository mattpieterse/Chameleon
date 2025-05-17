﻿using Microsoft.Extensions.Hosting;
using Spectre.Console.Cli;

namespace Terminal.Domain;

/// <summary>
/// Extension methods for <see cref="IHostBuilder"/>.
/// </summary>
public static class HostBuilderExtensions
{
#region Extensions

    /// <summary>
    /// Creates a new <see cref="CommandApp"/> instance using the extended
    /// <see cref="IHostBuilder"/> containing host configurations like services
    /// and environments. This allows Dependency Injection (DI) to be used with
    /// Spectre Console objects.
    /// </summary>
    /// <param name="builder">
    /// The <see cref="IHostBuilder"/> to extend.
    /// </param>
    /// <returns>
    /// <see cref="CommandApp"/>
    /// </returns>
    public static CommandApp BuildApp(this IHostBuilder builder) {
        var registrar = new SpectreTypeRegistrar(builder);
        var application = new CommandApp(registrar);

        return application;
    }

#endregion
}
