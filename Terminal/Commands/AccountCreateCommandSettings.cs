using JetBrains.Annotations;
using Spectre.Console.Cli;

namespace Terminal.Commands;

[UsedImplicitly]
public sealed class AccountCreateCommandSettings
    : CommandSettings
{
    [CommandArgument(0, "<name>")]
    public string Name { get; set; } = string.Empty;
}
