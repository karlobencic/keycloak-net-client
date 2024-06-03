using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.Users.Update;

internal sealed record UpdateUserRequestJsonData(string FirstName, string LastName, string Email, bool Enabled, string? Username)
{
    [JsonPropertyName("username")]
    public string? Username { get; private set; } = Username;

    [JsonPropertyName("firstName")]
    public string FirstName { get; private set; } = FirstName;

    [JsonPropertyName("lastName")]
    public string LastName { get; private set; } = LastName;

    [JsonPropertyName("email")]
    public string Email { get; private set; } = Email;

    [JsonPropertyName("enabled")]
    public bool Enabled { get; private set; } = Enabled;
}
