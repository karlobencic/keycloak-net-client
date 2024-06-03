using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Roles;

public sealed record GetClientRolesRequest(string EndpointAddress, string RealmName, string ProtectionApiToken, string ClientId)
    : KeycloakRequestBase(EndpointAddress, RealmName);
