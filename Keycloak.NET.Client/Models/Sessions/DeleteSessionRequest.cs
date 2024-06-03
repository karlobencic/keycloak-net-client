using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Sessions;

public sealed record DeleteSessionRequest(string EndpointAddress, string RealmName, string ProtectionApiToken, string SessionId)
    : KeycloakRequestBase(EndpointAddress, RealmName);
