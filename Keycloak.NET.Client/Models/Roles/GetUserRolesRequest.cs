using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Roles;

public sealed record GetUserRolesRequest(string EndpointAddress, string RealmName, string ProtectionApiToken, string Username)
    : KeycloakRequestBase(EndpointAddress, RealmName)
{
    public string? ClientId { get; }

    public GetUserRolesRequest(string endpointAddress, string realmName, string protectionApiToken, string username, string clientId)
        : this(endpointAddress, realmName, protectionApiToken, username)
    {
        ProtectionApiToken = protectionApiToken;
        Username = username;
        ClientId = clientId;
    }
}
