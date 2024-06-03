namespace NextLevelDev.Keycloak.Models.Roles;

public sealed record GetUserRolesResponse(IReadOnlyCollection<Role> Roles);
