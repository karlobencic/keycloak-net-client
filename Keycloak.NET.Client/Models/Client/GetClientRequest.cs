using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Client;

public sealed record GetClientRequest(string EndpointAddress, string RealmName, string ProtectionApiToken, string ClientId)
    : KeycloakRequestBase(EndpointAddress, RealmName);
