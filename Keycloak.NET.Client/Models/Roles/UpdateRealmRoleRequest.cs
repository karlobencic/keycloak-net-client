using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Roles;

public sealed record UpdateRealmRoleRequest(
    string EndpointAddress,
    string RealmName,
    string ProtectionApiToken,
    string ExistingRoleName,
    string NewRoleName,
    string Description = ""
) : KeycloakRequestBase(EndpointAddress, RealmName);
