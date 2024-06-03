using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.Users.Initialize;

internal sealed record UserCredentialJsonData(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("value")] string Value
);
