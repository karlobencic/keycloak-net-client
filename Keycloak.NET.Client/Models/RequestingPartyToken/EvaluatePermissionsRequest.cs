using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.RequestingPartyToken;

public sealed record EvaluatePermissionsRequest(string EndpointAddress, string RealmName, string UserAccessToken, string PermissionTicket)
    : KeycloakRequestBase(EndpointAddress, RealmName);
