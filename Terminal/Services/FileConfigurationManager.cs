using System.Text.Json;
using Terminal.Domain.Config;

namespace Terminal.Services;

public class FileConfigurationManager
    : IConfigurationManager
{
#region Fields

    private const string Path = "config.json";

    private readonly JsonSerializerOptions _serializerOptions = new() {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true
    };

#endregion

#region Inherited

    /// <inheritdoc cref="IConfigurationManager.Load"/>
    public Config Load() {
        EnsureConfigDirectoryExists();
        var json = File.ReadAllText(Path);
        return JsonSerializer.Deserialize<Config>(json, _serializerOptions)
               ?? new Config();
    }

    /// <inheritdoc cref="IConfigurationManager.Save"/>
    public void Save(Config config) {
        ArgumentNullException.ThrowIfNull(config);
        var json = JsonSerializer.Serialize(config, _serializerOptions);
        File.WriteAllText(Path, json);
    }

#endregion

#region Internals

    /// <summary>
    /// Creates an empty JSON file if one does not exist.
    /// </summary>
    private void EnsureConfigDirectoryExists() {
        if (File.Exists(Path)) {
            return;
        }

        var json = JsonSerializer.Serialize(new Config(), _serializerOptions);
        File.WriteAllText(Path, json);
    }

#endregion
}
