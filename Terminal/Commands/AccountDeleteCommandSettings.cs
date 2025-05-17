using Spectre.Console.Cli;

namespace Terminal.Commands;

public class AccountDeleteCommandSettings
    : CommandSettings
{
    [CommandArgument(0, "<name>")]
    public string Name { get; set; } = string.Empty;
}
