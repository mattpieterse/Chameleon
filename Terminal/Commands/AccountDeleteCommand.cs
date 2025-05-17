using Spectre.Console;
using Spectre.Console.Cli;
using Terminal.Domain.Config;
using Terminal.Services;
using Terminal.Validation;

namespace Terminal.Commands;

public sealed class AccountDeleteCommand(
    IConfigurationManager configManager
) : Command<AccountDeleteCommandSettings>
{
#region Inherited

    public override int Execute(
        CommandContext context, AccountDeleteCommandSettings settings
    ) {
        var config = configManager.Load();

        var accountIndex = config.Accounts.FindIndex((i) => i.Name == settings.Name);
        if (accountIndex < 0) {
            AnsiConsole.MarkupLineInterpolated($"[red]Could not find account <{settings.Name}>.[/]");
            return -1;
        }

        config.Accounts.RemoveAt(accountIndex);
        configManager.Save(config);
        AnsiConsole.MarkupLineInterpolated($"[green]Account <{settings.Name}> deleted successfully.[/]");
        return 0;
    }

#endregion
}
