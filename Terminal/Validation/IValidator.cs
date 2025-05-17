namespace Terminal.Validation;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T">
/// The object to validate.
/// </typeparam>
public interface IValidator<in T>
{
#region Contract

    void Validate(T instance);

#endregion
}
