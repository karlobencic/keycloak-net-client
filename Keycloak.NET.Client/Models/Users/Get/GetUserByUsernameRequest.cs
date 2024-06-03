using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Users.Get;

public sealed record GetUserByUsernameRequest(string EndpointAddress, string RealmName, string ProtectionApiToken, string Username)
    : KeycloakRequestBase(EndpointAddress, RealmName);
