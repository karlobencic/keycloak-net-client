using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Logout;

public sealed record LogoutRequest(
    string EndpointAddress,
    string RealmName,
    string Protocol,
    string AccessToken,
    string RefreshToken,
    string ClientId,
    string ClientSecret
) : KeycloakRequestBase(EndpointAddress, RealmName);
