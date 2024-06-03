using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Users.Initialize;

public sealed record InitializeUserRequest(
    string EndpointAddress,
    string RealmName,
    string ProtectionApiToken,
    Guid UserId,
    string Password,
    bool ShouldUpdatePassword
) : KeycloakRequestBase(EndpointAddress, RealmName);
