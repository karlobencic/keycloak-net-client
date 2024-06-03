namespace NextLevelDev.Keycloak.Models.Roles;

public sealed record GetRealmRoleByNameResponse(Guid Id, string Name, bool IsComposite, bool IsClientRole, string ContainerId, string Description);
