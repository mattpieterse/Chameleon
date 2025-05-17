using Spectre.Console;
using Terminal.Domain.Config;
using Terminal.Services;
using Terminal.Validation;

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
            AnsiConsole.MarkupLineInterpolated($"[red]Configuration directory is poorly formed.[/]");
            Environment.Exit(1);
        }
        catch (Exception e) {
            AnsiConsole.MarkupLineInterpolated($"[red]Could not load configuration: {e.Message}[/]");
            Environment.Exit(1);
        }

        try {
            var validator = new ConfigValidator();
            validator.Validate(_appConfiguration);
        }
        catch (Exception e) {
            AnsiConsole.MarkupLineInterpolated($"[red]Config Validation Error: {e.Message}[/]");
            Environment.Exit(1);
        }
    }

#endregion
}
