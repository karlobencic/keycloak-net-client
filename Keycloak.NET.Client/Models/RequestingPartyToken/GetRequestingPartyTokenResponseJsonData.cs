using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.RequestingPartyToken;

internal sealed record GetRequestingPartyTokenResponseJsonData(
    [property: JsonPropertyName("upgraded")] bool Upgraded,
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("refresh_token")] string RefreshToken,
    [property: JsonPropertyName("token_type")] string TokenType,
    [property: JsonPropertyName("expires_in")] int ExpiresIn,
    [property: JsonPropertyName("refresh_expires_in")] int RefreshExpiresIn,
    [property: JsonPropertyName("not-before-policy")] int NotBeforePolicy
);
