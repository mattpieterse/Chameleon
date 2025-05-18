using System.Net.Mail;
using JetBrains.Annotations;
using Spectre.Console;
using Spectre.Console.Cli;
using Terminal.Domain.Config;
using Terminal.Services;

namespace Terminal.Commands;

/// <summary>
/// Command to create an account.
/// </summary>
[UsedImplicitly]
public sealed class AccountCreateCommand(IConfigurationManager configManager)
    : Command<AccountCreateCommandSettings>
{
#region Fields

    private const int FailureCode = 1;
    private const int SuccessCode = 0;

    private record AccountInformation(
        string CommitAlias,
        string CommitEmail,
        string KeyDirectory
    );

#endregion

#region Inherited

    /// <inheritdoc cref="Command{T}.Execute(CommandContext, T)"/>   
    public override int Execute(
        CommandContext context, AccountCreateCommandSettings settings
    ) {
        var configuration = configManager.Load();
        
        if (configuration.Accounts.Exists((i) => i.Name == settings.Name)) {
            AnsiConsole.MarkupLineInterpolated($"[red]This account already exists.[/]");
            return FailureCode;
        }

        // Confirmation

        if (!CollectConfirmation(settings.Name)) {
            return FailureCode;
        }

        // UI Collection

        ShowTransactionBanner();

        var accountInformation = CollectInformation();
        if (accountInformation is null) {
            ShowTransactionCancel();
            return FailureCode;
        }

        ShowTransactionCloser();

        // Configuration

        configuration.Accounts.Add(
            new AccountSection {
                Name = settings.Name,
                Path = accountInformation.KeyDirectory,
                Configs = new AccountConfigs {
                    Alias = accountInformation.CommitAlias,
                    Email = accountInformation.CommitEmail
                }
            }
        );

        configManager.Save(configuration);
        AnsiConsole.MarkupLineInterpolated($"[green]Account created successfully.[/]");
        return SuccessCode;
    }

#endregion

#region Interface

    private static bool CollectConfirmation(string name) {
        var header = $"Ensure that you have created and registered the necessary SSH profiles for '{name}'.";
        var prompt = "\nContinue?";

        return AnsiConsole.Confirm(header + prompt);
    }


    private static AccountInformation? CollectInformation() {
        var commitAlias = AnsiConsole.Ask<string>("\nCommit Alias:");
        if (ContainsExitFlag(commitAlias)) {
            return null;
        }

        string commitEmail;
        try {
            commitEmail = AnsiConsole.Prompt(new TextPrompt<string>("\nCommit Email:")
                .Validate(ValidateEmail)
            );
        }
        catch (OperationCanceledException) {
            return null;
        }

        string keyDirectory;
        try {
            keyDirectory = AnsiConsole.Prompt(
                new TextPrompt<string>("\nPath to SSH File (Public):")
                    .Validate(ValidatePaths)
            );
        }
        catch (OperationCanceledException) {
            return null;
        }

        // Construct Account

        return new AccountInformation(
            commitAlias, commitEmail, keyDirectory
        );
    }

#endregion

#region Internals

    private static ValidationResult ValidateEmail(string input) {
        if (ContainsExitFlag(input)) {
            throw new OperationCanceledException();
        }

        // Validation

        try {
            _ = new MailAddress(input);
            return ValidationResult.Success();
        }
        catch {
            return ValidationResult.Error("[red]Invalid email address.[/]");
        }
    }


    private static ValidationResult ValidatePaths(string input) {
        if (input.Length == 1 && input.ToLower().Contains('q')) {
            throw new OperationCanceledException();
        }

        // Validation
        
        return !input.EndsWith(".pub", StringComparison.OrdinalIgnoreCase) 
            ? ValidationResult.Error("[red]File must be a public key file (.pub).[/]") 
            : ValidationResult.Success();
    }


    private static bool ContainsExitFlag(string input) =>
        ((input.Length == 1) && (input.ToLower().Contains('q')));


    private static void ShowTransactionBanner() =>
        AnsiConsole.WriteLine($"\n-- Chameleon (Insert) {new string('-', 60)}\n");


    private static void ShowTransactionCloser() =>
        AnsiConsole.WriteLine($"{Environment.NewLine}{new string('-', 82)}");


    private static void ShowTransactionCancel() {
        ShowTransactionCloser();
        AnsiConsole.MarkupLine("[red]Session cancelled.[/]");
    }

#endregion
}
