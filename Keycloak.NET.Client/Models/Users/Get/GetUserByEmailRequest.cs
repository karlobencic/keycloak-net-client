using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Users.Get;

public sealed record GetUserByEmailRequest(string EndpointAddress, string RealmName, string ProtectionApiToken, string Email)
    : KeycloakRequestBase(EndpointAddress, RealmName);
