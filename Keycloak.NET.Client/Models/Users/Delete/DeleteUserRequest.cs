using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Users.Delete;

public sealed record DeleteUserRequest(string EndpointAddress, string RealmName, string ProtectionApiToken, Guid Id)
    : KeycloakRequestBase(EndpointAddress, RealmName);
