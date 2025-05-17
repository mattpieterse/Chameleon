using System.Net.Mail;
using Spectre.Console;
using Spectre.Console.Cli;
using Terminal.Domain.Config;
using Terminal.Services;
using Terminal.Validation;

namespace Terminal.Commands;

public sealed class AccountCreateCommand(
    IConfigurationManager configManager
) : Command<AccountCreateCommandSettings>
{
#region Inherited

    public override int Execute(
        CommandContext context, AccountCreateCommandSettings settings
    ) {
        var config = configManager.Load();

        // 

        if (config.Accounts.Exists((i) => i.Name == settings.Name)) {
            AnsiConsole.MarkupLineInterpolated($"[red]This account profile already exists.[/]");
            return -1;
        }

        //

        var path = AnsiConsole.Ask<string>("Path to SSH File (Public):");

        var alias = AnsiConsole.Ask<string>("GIT Alias:");
        var email = AnsiConsole.Prompt(
            new TextPrompt<string>("GIT Email:")
                .Validate((input) => {
                    try {
                        _ = new MailAddress(input);
                        return ValidationResult.Success();
                    }
                    catch {
                        return ValidationResult.Error("[red]Invalid email address.[/]");
                    }
                })
        );

        //

        config.Accounts.Add(
            new AccountSection {
                Name = settings.Name,
                Path = path,

                Configs = new AccountConfigs {
                    Alias = alias,
                    Email = email
                }
            }
        );

        configManager.Save(config);
        AnsiConsole.MarkupLineInterpolated($"[green]Account <{settings.Name}> created successfully.[/]");
        return 0;
    }

#endregion
}
