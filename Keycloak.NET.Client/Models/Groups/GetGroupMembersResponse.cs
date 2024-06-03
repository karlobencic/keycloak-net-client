using NextLevelDev.Keycloak.Models.Users;

namespace NextLevelDev.Keycloak.Models.Groups;

public sealed record GetGroupMembersResponse(IReadOnlyCollection<User> Users);
