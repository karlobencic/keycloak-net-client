using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Groups;

public sealed record GetGroupMembersRequest(
    string EndpointAddress,
    string RealmName,
    string GroupName,
    string ProtectionApiToken,
    string? SubGroupName = null
) : KeycloakRequestBase(EndpointAddress, RealmName)
{
    public int? Limit { get; init; }
}
