using System.Diagnostics;
using Spectre.Console;
using Spectre.Console.Cli;
using Terminal.Domain.Config;
using Terminal.Services;
using Terminal.Validation;

namespace Terminal.Commands;

/// <summary>
/// Command to set the active account.
/// </summary>
public sealed class AccountUseCommand(
    IConfigurationManager configManager,
    IValidator<Config> configValidator
) : Command<AccountUseCommandSettings>
{
#region Inherited

    /// <inheritdoc cref="Command{T}.Execute(CommandContext, T)"/>   
    public override int Execute(
        CommandContext context, AccountUseCommandSettings settings
    ) {
        var config = configManager.Load();
        configValidator.Validate(config);

        var account = config.Accounts.Find((a) => a.Name == settings.Name);
        if (account is null) {
            AnsiConsole.MarkupLineInterpolated($"[red]Account <{settings.Name}> not found in config.[/]");
            return -1;
        }

        var gitConfigs = new[] {
            ("user.name", account.Configs.Alias),
            ("user.email", account.Configs.Email),
            ("user.signingkey", account.Path),
            ("core.sshCommand", $"ssh -i {account.Path.Replace(".pub", string.Empty)}"),
            ("commit.gpgsign", "true"),
            ("gpg.format", "ssh")
        };

        foreach (var (key, value) in gitConfigs) {
            var result = SetGitConfig(key, value);
            if (result != 0) {
                AnsiConsole.MarkupLineInterpolated($"[red]Failed to set git config <{key}>.[/]");
                return result;
            }
        }

        AnsiConsole.MarkupLineInterpolated($"[green]Account <{settings.Name}> set as active.[/]");
        AnsiConsole.MarkupLine($"\n[blue]Testing SSH connection for '{settings.Name}'...[/]");
        if (RunSshTest(settings.Name)) {
            AnsiConsole.MarkupLine("[green]SSH connection successful![/]");
            return 0;
        }
        else {
            AnsiConsole.MarkupLine("[red]SSH authentication failed.[/]");
            return 1;
        }
    }

#endregion

#region Internals

    private static int SetGitConfig(string key, string value) {
        var request = new ProcessStartInfo("git", $"config --global {key} \"{value}\"") {
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        using var process = Process.Start(request);
        process?.WaitForExit();

        return process?.ExitCode
               ?? -1;
    }

    private static bool RunSshTest(string account) {
        var host = $"git@github.com-{account}";
        var request = new ProcessStartInfo("ssh", $"-T {host}") {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        using var process = Process.Start(request);
        var result = process?.StandardError.ReadToEnd();
        process?.WaitForExit();
        return result?.Contains("successfully authenticated")
               ?? false;
    }

#endregion
}
