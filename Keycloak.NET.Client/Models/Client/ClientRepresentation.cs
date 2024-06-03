using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.Client;

public sealed record ClientRepresentation(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("clientid")] string ClientId
);
