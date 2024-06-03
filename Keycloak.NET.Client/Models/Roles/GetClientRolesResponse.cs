namespace NextLevelDev.Keycloak.Models.Roles;

public sealed record GetClientRolesResponse(IReadOnlyCollection<Role> Roles);
