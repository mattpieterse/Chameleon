using JetBrains.Annotations;
using Spectre.Console;
using Spectre.Console.Cli;
using Terminal.Domain.Config;
using Terminal.Services;

namespace Terminal.Commands;

/// <summary>
/// Command to delete an account.
/// </summary>
[UsedImplicitly]
public sealed class AccountDeleteCommand(IConfigurationManager configManager)
    : Command<AccountDeleteCommandSettings>
{
#region Fields

    private const int FailureCode = 1;
    private const int SuccessCode = 0;

#endregion

#region Inherited

    /// <inheritdoc cref="Command{T}.Execute(CommandContext, T)"/>   
    public override int Execute(
        CommandContext context, AccountDeleteCommandSettings settings
    ) {
        var configuration = configManager.Load();
        
        var accountIndex = configuration.Accounts.FindIndex((i) => i.Name == settings.Name);
        if (accountIndex != SuccessCode) {
            AnsiConsole.MarkupLineInterpolated($"[red]Could not find account <{settings.Name}>.[/]");
            return FailureCode;
        }

        configuration.Accounts.RemoveAt(accountIndex);
        configManager.Save(configuration);
        AnsiConsole.MarkupLineInterpolated($"[green]Account <{settings.Name}> deleted successfully.[/]");
        return SuccessCode;
    }

#endregion
}
