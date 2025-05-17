using Spectre.Console;
using Spectre.Console.Cli;
using Terminal.Services;

namespace Terminal.Commands;

public sealed class AccountDeleteCommand
    : Command<AccountDeleteCommandSettings>
{
#region Inherited

    public override int Execute(
        CommandContext context, AccountDeleteCommandSettings settings
    ) {
        var manager = new FileConfigurationManager("config.json");
        var config = manager.Load();

        var accountIndex = config.Accounts.FindIndex((i) => i.Name == settings.Name);
        if (accountIndex < 0) {
            AnsiConsole.MarkupLineInterpolated($"[red]Could not find account <{settings.Name}>.[/]");
            return -1;
        }
        
        config.Accounts.RemoveAt(accountIndex);
        manager.Save(config);
        AnsiConsole.MarkupLineInterpolated($"[green]Account <{settings.Name}> deleted successfully.[/]");
        return 0;
    }

#endregion
}
