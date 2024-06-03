using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Users.ChangePassword;

public sealed record ChangePasswordRequest(string EndpointAddress, string RealmName, string ProtectionApiToken, string UserId, string Password)
    : KeycloakRequestBase(EndpointAddress, RealmName);
