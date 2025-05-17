using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console.Cli;
using Terminal.Commands;
using Terminal.Domain;
using Terminal.Domain.Config;
using Terminal.Services;
using Terminal.Validation;

namespace Terminal;

internal static class App
{
#region Lifecycle

    /// <summary>
    /// Entry point for the application.
    /// </summary>
    /// <param name="args">
    /// Arguments parsed from the command-line upon execution of the application
    /// via a terminal. Used by the <see cref="Spectre.Console.Cli.CommandApp"/>
    /// to execute commands with arguments.
    /// </param>
    /// <returns>
    /// <see cref="Environment.ExitCode"/>
    /// </returns>
    private static void Main(string[] args) {
    #region Host

        var builder = Host.CreateDefaultBuilder(args);

        // Dependency Injection (DI)
        builder.ConfigureServices(static (_, services) => {
            services.AddSingleton<IConfigurationManager, FileConfigurationManager>();
            services.AddSingleton<IValidator<Config>, ConfigurationValidator>();
        });

    #endregion

    #region Configuration

        var app = builder.BuildApp();

        // Application Configuration
        app.Configure((options) => {
            options.SetApplicationName("Chameleon (4Git)");
            options.SetApplicationVersion("v0.0.0 (beta)");

            // Commands

            options.AddBranch("account", (group) => {
                group.AddCommand<AccountCreateCommand>("create");
                group.AddCommand<AccountDeleteCommand>("delete");
                group.AddCommand<AccountUseCommand>("use");
            });
        });

        app.Run(args);

    #endregion
    }

#endregion
}
