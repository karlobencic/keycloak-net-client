using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Roles;

public sealed record UserRolesRequest(
    string EndpointAddress,
    string RealmName,
    string ProtectionApiToken,
    string ClientId,
    string Username,
    string[] Roles
) : KeycloakRequestBase(EndpointAddress, RealmName);
