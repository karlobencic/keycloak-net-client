using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Roles;

public sealed record GetUsersWithRoleRequest(string EndpointAddress, string RealmName, string ProtectionApiToken, string Role)
    : KeycloakRequestBase(EndpointAddress, RealmName);
