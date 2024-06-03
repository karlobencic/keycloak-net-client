using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Roles;

public sealed record DeleteRealmRoleRequest(string EndpointAddress, string RealmName, string ProtectionApiToken, string RoleName)
    : KeycloakRequestBase(EndpointAddress, RealmName);
