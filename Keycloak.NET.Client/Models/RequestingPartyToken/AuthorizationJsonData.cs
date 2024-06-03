using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.RequestingPartyToken;

internal sealed record AuthorizationJsonData([property: JsonPropertyName("permissions")] PermissionJsonData[] Permissions);

internal sealed record PermissionJsonData(
    [property: JsonPropertyName("rsname")] string ResourceName,
    [property: JsonPropertyName("scopes")] string[] Scopes
);
