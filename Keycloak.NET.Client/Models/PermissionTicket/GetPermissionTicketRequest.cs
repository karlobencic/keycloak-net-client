using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.PermissionTicket;

public sealed record GetPermissionTicketRequest : KeycloakRequestBase
{
    public string ProtectionApiToken { get; }

    public string[] ResourceIds { get; }

    public string? ResourceScope { get; }

    public GetPermissionTicketRequest(
        string endpointAddress,
        string realmName,
        string protectionApiToken,
        string resourceId,
        string resourceScope
    )
        : base(endpointAddress, realmName)
    {
        ProtectionApiToken = protectionApiToken;
        ResourceIds = [resourceId];
        ResourceScope = resourceScope;
    }

    public GetPermissionTicketRequest(string endpointAddress, string realmName, string protectionApiToken, string[] resourceIds)
        : base(endpointAddress, realmName)
    {
        ProtectionApiToken = protectionApiToken;
        ResourceIds = resourceIds;
    }
}
