using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.ProtectionApiToken;

public sealed record GetProtectionApiTokenResponse(
    [property: JsonPropertyName("access_token")] string Token,
    [property: JsonPropertyName("expires_in")] int AccessTokenExpireSeconds,
    [property: JsonPropertyName("token_type")] string TokenType,
    [property: JsonPropertyName("scope")] string Scope
);
