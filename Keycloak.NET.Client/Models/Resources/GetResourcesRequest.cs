using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Resources;

public sealed record GetResourcesRequest(string EndpointAddress, string RealmName, string ProtectionApiToken)
    : KeycloakRequestBase(EndpointAddress, RealmName);
