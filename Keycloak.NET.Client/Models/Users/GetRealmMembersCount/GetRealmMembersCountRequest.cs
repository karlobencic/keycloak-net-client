using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Users.GetRealmMembersCount;

public sealed record GetRealmMembersCountRequest(string EndpointAddress, string RealmName, string ProtectionApiToken)
    : KeycloakRequestBase(EndpointAddress, RealmName);
