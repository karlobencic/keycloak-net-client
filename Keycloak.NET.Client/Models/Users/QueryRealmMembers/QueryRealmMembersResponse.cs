namespace NextLevelDev.Keycloak.Models.Users.QueryRealmMembers;

public sealed record QueryRealmMembersResponse(IReadOnlyCollection<User> Users);
