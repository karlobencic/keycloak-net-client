using NextLevelDev.Keycloak.Models.Roles;
using NextLevelDev.Keycloak.Utility.Extensions;
using NextLevelDev.Keycloak.Utility.HttpClient;

namespace NextLevelDev.Keycloak.Clients;

internal class RolesClient(IHttpClientUtility httpClientUtility) : KeycloakBaseClient(httpClientUtility), IRolesClient
{
    /// <inheritdoc />
    public async Task<CreateRealmRoleResponse> CreateRealmRole(CreateRealmRoleRequest request)
    {
        var roleData = new CreateRealmRoleRequestJsonData(request.RoleName, request.Description);

        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/roles";
        var location = await HttpClientUtility.PostAsyncAndGetLocation(requestUrl, request.ProtectionApiToken, roleData);
        string roleName = location.ToString().Split('/').Last();

        return new CreateRealmRoleResponse(roleName);
    }

    /// <inheritdoc />
    public async Task<GetRealmRoleByNameResponse> GetRealmRoleByName(GetRealmRoleByNameRequest request)
    {
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/roles/{request.RoleName}";
        var roleRepresentation = await HttpClientUtility.GetAsync<RoleRepresentation>(requestUrl, request.ProtectionApiToken);

        return new GetRealmRoleByNameResponse(
            roleRepresentation.Id,
            roleRepresentation.Name,
            roleRepresentation.IsComposite,
            roleRepresentation.IsClientRole,
            roleRepresentation.ContainerId,
            roleRepresentation.Description
        );
    }

    /// <inheritdoc />
    public async Task<GetRealmRolesResponse> GetRealmRoles(GetRealmRolesRequest request)
    {
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/roles";
        var realmRoles = await HttpClientUtility.GetAsync<RoleRepresentation[]>(requestUrl, request.ProtectionApiToken);
        var roles = realmRoles.Select(x => new Role(x.Id, x.Name, x.Description)).ToList();

        return new GetRealmRolesResponse(roles.ToReadOnlyCollection());
    }

    /// <inheritdoc />
    public async Task UpdateRealmRole(UpdateRealmRoleRequest request)
    {
        var roleData = new CreateRealmRoleRequestJsonData(request.NewRoleName, request.Description);
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/roles/{request.ExistingRoleName}";

        await HttpClientUtility.PutAsync(requestUrl, request.ProtectionApiToken, roleData);
    }

    /// <inheritdoc />
    public async Task DeleteRealmRole(DeleteRealmRoleRequest request)
    {
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/roles/{request.RoleName}";

        await HttpClientUtility.DeleteAsync(requestUrl, request.ProtectionApiToken);
    }

    /// <inheritdoc />
    public async Task<GetClientRolesResponse> GetClientRoles(GetClientRolesRequest request)
    {
        var client = await GetClientById(request.EndpointAddress, request.RealmName, request.ProtectionApiToken, request.ClientId);

        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/clients/{client.Id}/roles";
        var rolesRepresentation = await HttpClientUtility.GetAsync<RoleRepresentation[]>(requestUrl, request.ProtectionApiToken);
        var roles = rolesRepresentation.Select(x => new Role(x.Id, x.Name, x.Description)).ToList();

        return new GetClientRolesResponse(roles.ToReadOnlyCollection());
    }
}
