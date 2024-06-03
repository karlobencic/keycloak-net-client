using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Roles;

public sealed record CreateRealmRoleRequest(
    string EndpointAddress,
    string RealmName,
    string ProtectionApiToken,
    string RoleName,
    string Description = ""
) : KeycloakRequestBase(EndpointAddress, RealmName);
