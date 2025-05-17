namespace Terminal.Domain.Config;

public class AccountSection
{
#region JSON

    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public AccountConfigs Configs { get; set; } = new();

#endregion
}
