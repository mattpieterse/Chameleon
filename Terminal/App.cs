using Spectre.Console;
using Terminal.Domain.Config;
using Terminal.Services;

namespace Terminal;

internal static class App
{
#region Fields

    private static Config? _appConfiguration;

#endregion

#region Lifecycle

    private static void Main(string[] args) {
        var config = new FileConfigurationManager("config.json");
        try {
            _appConfiguration = config.Load();
        }
        catch (ArgumentException) {
            AnsiConsole.MarkupLine($"[red]Configuration directory is poorly formed.[/]");
            Environment.Exit(1);
        }
        catch (Exception e) {
            AnsiConsole.MarkupLine($"[red]Could not load configuration: {e.Message}[/]");
            Environment.Exit(1);
        }
    }

#endregion
}
