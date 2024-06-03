namespace NextLevelDev.Keycloak.Models.Roles;

public sealed record GetRealmRolesResponse(IReadOnlyCollection<Role> Roles);
