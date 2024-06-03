using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Users.Email;

public sealed record SendVerifyEmailRequest(string EndpointAddress, string RealmName, string ProtectionApiToken, string UserId)
    : KeycloakRequestBase(EndpointAddress, RealmName);
