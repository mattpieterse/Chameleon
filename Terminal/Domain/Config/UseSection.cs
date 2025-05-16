namespace Terminal.Domain.Config;

public class UseSection
{
#region JSON

    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public UseConfigs Configs { get; set; } = new();

#endregion
}
