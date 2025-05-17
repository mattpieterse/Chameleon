using Terminal.Domain.Config;

namespace Terminal.Services;

/// <summary>
/// Contract to define a service to manage application configurations.
/// </summary>
public interface IConfigurationManager
{
#region Contract

    /// <summary>
    /// Loads the configuration.
    /// </summary>
    /// <remarks>
    /// If the configuration does not exist in the location defined by the
    /// contract inheritor, then a new one will be created with default values
    /// and returned.
    /// </remarks>
    /// <returns>
    /// The JSON configuration.
    /// </returns>
    Config Load();


    /// <summary>
    /// Saves the configuration.
    /// </summary>
    /// <param name="config">
    /// The configuration POCO to write.
    /// </param>
    void Save(Config config);

#endregion
}
