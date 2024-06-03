using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Users.GetRealmMembers;

public sealed record GetRealmMembersRequest(string EndpointAddress, string RealmName, string ProtectionApiToken)
    : KeycloakRequestBase(EndpointAddress, RealmName)
{
    public int? Limit { get; init; }
}
