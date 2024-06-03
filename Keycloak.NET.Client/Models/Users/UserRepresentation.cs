using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.Users;

internal sealed record UserRepresentation(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("firstName")] string FirstName,
    [property: JsonPropertyName("lastName")] string LastName,
    [property: JsonPropertyName("username")] string UserName,
    [property: JsonPropertyName("enabled")] bool Enabled,
    [property: JsonPropertyName("emailVerified")] bool EmailVerified,
    [property: JsonPropertyName("groups")] Guid[] Groups,
    [property: JsonPropertyName("attributes")] Dictionary<string, string[]> Attributes
);
