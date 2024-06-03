using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Roles;

public sealed record GetRealmRolesRequest(string EndpointAddress, string RealmName, string ProtectionApiToken)
    : KeycloakRequestBase(EndpointAddress, RealmName);
