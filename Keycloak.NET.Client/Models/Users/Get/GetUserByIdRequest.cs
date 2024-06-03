using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Users.Get;

public sealed record GetUserByIdRequest(string EndpointAddress, string RealmName, string ProtectionApiToken, Guid UserId)
    : KeycloakRequestBase(EndpointAddress, RealmName);
