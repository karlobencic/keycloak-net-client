using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.Groups;

internal sealed record GroupRepresentation(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("path")] string Path,
    [property: JsonPropertyName("realmRoles")] string[] RealmRoles,
    [property: JsonPropertyName("subGroups")] GroupRepresentation[] SubGroups
);
