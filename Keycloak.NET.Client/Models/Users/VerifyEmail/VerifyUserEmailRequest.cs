using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Users.VerifyEmail;

public sealed record VerifyUserEmailRequest(string EndpointAddress, string RealmName, string ProtectionApiToken, Guid UserId, bool IsEmailVerified)
    : KeycloakRequestBase(EndpointAddress, RealmName);
