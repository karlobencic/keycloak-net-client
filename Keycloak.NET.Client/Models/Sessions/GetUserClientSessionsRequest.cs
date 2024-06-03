using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Sessions;

public sealed record GetUserClientSessionsRequest(
    string EndpointAddress,
    string RealmName,
    string ClientId,
    string ProtectionApiToken,
    string UserId
) : KeycloakRequestBase(EndpointAddress, RealmName);
