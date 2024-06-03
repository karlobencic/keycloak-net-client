using NextLevelDev.Keycloak.Models.Roles;

namespace NextLevelDev.Keycloak.Clients;

public interface IRolesClient : IKeycloakBaseClient
{
    /// <summary>
    /// Creates role
    /// </summary>
    Task<CreateRealmRoleResponse> CreateRealmRole(CreateRealmRoleRequest request);

    /// <summary>
    /// Get role by name
    /// </summary>
    Task<GetRealmRoleByNameResponse> GetRealmRoleByName(GetRealmRoleByNameRequest request);

    /// <summary>
    /// Update role by name
    /// </summary>
    Task UpdateRealmRole(UpdateRealmRoleRequest request);

    /// <summary>
    /// Delete role by name
    /// </summary>
    Task DeleteRealmRole(DeleteRealmRoleRequest request);

    /// <summary>
    /// Returns list of roles for given client
    /// </summary>
    Task<GetClientRolesResponse> GetClientRoles(GetClientRolesRequest request);

    /// <summary>
    /// Returns list of roles for given realm
    /// </summary>
    Task<GetRealmRolesResponse> GetRealmRoles(GetRealmRolesRequest request);
}
