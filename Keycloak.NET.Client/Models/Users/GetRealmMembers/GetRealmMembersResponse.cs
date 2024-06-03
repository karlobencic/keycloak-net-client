namespace NextLevelDev.Keycloak.Models.Users.GetRealmMembers;

public sealed record GetRealmMembersResponse(IReadOnlyCollection<User> Users);
