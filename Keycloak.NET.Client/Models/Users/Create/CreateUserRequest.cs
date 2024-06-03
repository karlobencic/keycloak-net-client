using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Users.Create;

public sealed record CreateUserRequest(
    string EndpointAddress,
    string RealmName,
    string ProtectionApiToken,
    string Username,
    string FirstName,
    string LastName,
    string Email
) : KeycloakRequestBase(EndpointAddress, RealmName);
