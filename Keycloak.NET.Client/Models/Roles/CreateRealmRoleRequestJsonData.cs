using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.Roles;

public sealed record CreateRealmRoleRequestJsonData(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")] string Description
);
