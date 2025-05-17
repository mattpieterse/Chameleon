using Spectre.Console.Cli;
using Terminal.Commands;
using Terminal.Domain.Config;

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
    private static int Main(string[] args) {
        var app = new CommandApp();

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

        return app.Run(args);
    }

#endregion
}
