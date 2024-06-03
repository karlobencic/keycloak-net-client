using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.PermissionTicket;

internal sealed record GetPermissionTicketRequestJsonData([property: JsonPropertyName("resource_id")] string ResourceId)
{
    [JsonPropertyName("resource_scopes")]
    public string[]? ResourceScopes { get; }

    public GetPermissionTicketRequestJsonData(string resourceId, string resourceScope)
        : this(resourceId)
    {
        ResourceScopes = [resourceScope];
    }
}
