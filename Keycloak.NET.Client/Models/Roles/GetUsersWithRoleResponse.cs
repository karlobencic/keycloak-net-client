using NextLevelDev.Keycloak.Models.Users;

namespace NextLevelDev.Keycloak.Models.Roles;

public sealed record GetUsersWithRoleResponse(IReadOnlyCollection<User> Users);
