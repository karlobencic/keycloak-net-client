using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Users.ForgotPassword;

public sealed record ForgotPasswordRequest(string EndpointAddress, string RealmName, string ProtectionApiToken, string UserId)
    : KeycloakRequestBase(EndpointAddress, RealmName)
{
    public string[] RequiredActions { get; } = ["UPDATE_PASSWORD"];
}
