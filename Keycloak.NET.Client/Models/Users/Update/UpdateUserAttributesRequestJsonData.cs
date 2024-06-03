using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.Users.Update;

internal sealed record UpdateUserAttributesRequestJsonData(
    [property: JsonPropertyName("attributes")] Dictionary<string, string> Attributes
);
