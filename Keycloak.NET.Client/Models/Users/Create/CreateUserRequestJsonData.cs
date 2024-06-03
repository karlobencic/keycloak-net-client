using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.Users.Create;

internal sealed record CreateUserRequestJsonData(
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("firstName")] string FirstName,
    [property: JsonPropertyName("lastName")] string LastName,
    [property: JsonPropertyName("email")] string Email
)
{
    [JsonPropertyName("emailVerified")]
    public bool EmailVerified { get; }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; }

    [JsonPropertyName("requiredActions")]
    public string[] RequiredActions { get; } = ["VERIFY_EMAIL", "UPDATE_PASSWORD"];
}
