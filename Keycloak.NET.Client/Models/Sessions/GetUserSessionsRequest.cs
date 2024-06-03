using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Sessions;

public sealed record GetUserSessionsRequest(string EndpointAddress, string RealmName, string ProtectionApiToken, string UserId)
    : KeycloakRequestBase(EndpointAddress, RealmName);
