using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Users.QueryRealmMembers;

public sealed record QueryRealmMembersRequest(string EndpointAddress, string RealmName, string ProtectionApiToken)
    : KeycloakRequestBase(EndpointAddress, RealmName)
{
    public string? Email { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string? Username { get; init; }

    public string? Search { get; init; }

    public int? Limit { get; init; }

    public int? First { get; init; }
}
