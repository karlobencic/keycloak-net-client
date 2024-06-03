using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Error;

public sealed record ErrorResponseJsonData(
    [property: JsonPropertyName("error")] string Error,
    [property: JsonPropertyName("error_description")] string ErrorDescription
);
