using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Groups;

public sealed record JoinUserToGroupsRequest(string EndpointAddress, string RealmName, string ProtectionApiToken, Guid UserId, string[] Groups)
    : KeycloakRequestBase(EndpointAddress, RealmName);
