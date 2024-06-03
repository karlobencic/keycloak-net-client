using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.Roles;

internal sealed record RoleRepresentation(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("containerId")] string ContainerId,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("composite")] bool IsComposite,
    [property: JsonPropertyName("clientRole")] bool IsClientRole
);
