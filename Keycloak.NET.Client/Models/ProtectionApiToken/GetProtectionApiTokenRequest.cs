using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.ProtectionApiToken;

public sealed record GetProtectionApiTokenRequest(
    string EndpointAddress,
    string RealmName,
    string ClientId,
    string ClientSecret,
    string? Scope = null
) : KeycloakRequestBase(EndpointAddress, RealmName);
