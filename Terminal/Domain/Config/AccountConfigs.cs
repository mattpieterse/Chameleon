namespace Terminal.Domain.Config;

/// <summary>
/// POCO for the application configuration schema.
/// </summary>
public class AccountConfigs
{
#region JSON

    public string Alias { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;

#endregion
}
