namespace Terminal.Domain.Config;

/// <summary>
/// POCO for the application configuration schema.
/// </summary>
public class AccountSection
{
#region JSON

    public string Name { get; init; } = string.Empty;
    public string Path { get; init; } = string.Empty;
    public AccountConfigs Configs { get; init; } = new();

#endregion
}
