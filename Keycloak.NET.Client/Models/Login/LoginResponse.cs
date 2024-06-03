using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.Login;

public sealed record LoginResponse(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("refresh_token")] string RefreshToken,
    int AccessTokenExpireSeconds,
    int RefreshTokenExpireSeconds,
    string TokenType,
    ulong NotBeforePolicy,
    string SessionState,
    string Scope
);
