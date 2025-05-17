namespace Terminal.Domain.Config;

/// <summary>
/// POCO for the application configuration schema.
/// </summary>
public class Config
{
#region JSON

    public List<AccountSection> Accounts { get; private init; } = [];

#endregion
}
