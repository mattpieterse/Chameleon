using System.Net.Mail;
using Terminal.Domain.Config;

namespace Terminal.Validation;

public class ConfigValidator
    : IValidator<Config>
{
#region Inherited

    public void Validate(Config instance) {
        if (instance is null) {
            throw new ArgumentNullException(nameof(instance));
        }

        if (instance.Uses.Count == 0) {
            throw new InvalidOperationException("No git accounts have been configured.");       
        }

        // Validate that fields match the expected type and are not empty
        for (var i = 0; i < instance.Uses.Count; i++) {
            var item = instance.Uses[i];

            if (string.IsNullOrWhiteSpace(item.Name)) {
                throw new InvalidOperationException($"Account[{i}] {nameof(item.Name).ToLower()} cannot be empty.]");
            }

            if (string.IsNullOrWhiteSpace(item.Path)) {
                throw new InvalidOperationException($"Account[{i}] {nameof(item.Path).ToLower()} cannot be empty.]");
            }

            if (string.IsNullOrWhiteSpace(item.Configs.Alias)) {
                throw new InvalidOperationException(
                    $"Account[{i}] {nameof(item.Configs.Alias).ToLower()} cannot be empty.]");
            }

            if (string.IsNullOrWhiteSpace(item.Configs.Email)) {
                throw new InvalidOperationException(
                    $"Account[{i}] {nameof(item.Configs.Email).ToLower()} cannot be empty.]");
            }

            // Validate email formatting
            try {
                _ = new MailAddress(item.Configs.Email);
            }
            catch (Exception e) {
                throw new InvalidOperationException(
                    $"Account[{i}] {nameof(item.Configs.Email).ToLower()} is not a valid email address.");
            }
        }
    }

#endregion
}
