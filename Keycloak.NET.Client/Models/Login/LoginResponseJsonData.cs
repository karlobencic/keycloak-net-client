using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.Login;

public sealed record LoginResponseJsonData(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("refresh_token")] string RefreshToken,
    [property: JsonPropertyName("expires_in")] int AccessTokenExpireSeconds,
    [property: JsonPropertyName("refresh_expires_in")] int RefreshTokenExpireSeconds,
    [property: JsonPropertyName("token_type")] string TokenType,
    [property: JsonPropertyName("not-before-policy")] ulong NotBeforePolicy,
    [property: JsonPropertyName("session_state")] string SessionState,
    [property: JsonPropertyName("scope")] string Scope
);
