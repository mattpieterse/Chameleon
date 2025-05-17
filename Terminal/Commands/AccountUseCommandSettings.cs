using Spectre.Console.Cli;

namespace Terminal.Commands;

public sealed class AccountUseCommandSettings
    : CommandSettings
{
    [CommandArgument(0, "<name>")]
    public string Name { get; set; } = string.Empty;
}
