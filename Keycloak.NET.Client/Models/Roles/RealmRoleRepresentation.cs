using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.Roles;

internal sealed record RealmRoleRepresentation(
    [property: JsonPropertyName("realmMappings")] IEnumerable<RoleRepresentation> RoleRepresentations
);
