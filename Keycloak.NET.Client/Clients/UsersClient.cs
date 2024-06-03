using System.Text;
using System.Web;
using NextLevelDev.Keycloak.Models.Groups;
using NextLevelDev.Keycloak.Models.Roles;
using NextLevelDev.Keycloak.Models.Sessions;
using NextLevelDev.Keycloak.Models.Users;
using NextLevelDev.Keycloak.Models.Users.ChangePassword;
using NextLevelDev.Keycloak.Models.Users.Create;
using NextLevelDev.Keycloak.Models.Users.Delete;
using NextLevelDev.Keycloak.Models.Users.Email;
using NextLevelDev.Keycloak.Models.Users.ForgotPassword;
using NextLevelDev.Keycloak.Models.Users.Get;
using NextLevelDev.Keycloak.Models.Users.GetRealmMembers;
using NextLevelDev.Keycloak.Models.Users.GetRealmMembersCount;
using NextLevelDev.Keycloak.Models.Users.Initialize;
using NextLevelDev.Keycloak.Models.Users.QueryRealmMembers;
using NextLevelDev.Keycloak.Models.Users.Update;
using NextLevelDev.Keycloak.Models.Users.VerifyEmail;
using NextLevelDev.Keycloak.Utility.Extensions;
using NextLevelDev.Keycloak.Utility.HttpClient;

namespace NextLevelDev.Keycloak.Clients;

internal class UsersClient(IHttpClientUtility httpClientUtility) : KeycloakBaseClient(httpClientUtility), IUsersClient
{
    /// <inheritdoc />
    public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
    {
        var userData = new CreateUserRequestJsonData(request.Username, request.FirstName, request.LastName, request.Email);

        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users";
        var location = await HttpClientUtility.PostAsyncAndGetLocation(requestUrl, request.ProtectionApiToken, userData);
        var userId = Guid.Parse(location.ToString().Split('/').Last());

        return new CreateUserResponse(userId);
    }

    /// <inheritdoc />
    public async Task<CreateUserWithPasswordResponse> CreateUserWithPassword(CreateUserWithPasswordRequest request)
    {
        var userData = new CreateUserWithPasswordRequestJsonData(
            request.Username,
            request.Password,
            request.FirstName,
            request.LastName,
            request.Email,
            request.Enabled,
            request.ShouldUpdatePassword
        );

        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users";
        var location = await HttpClientUtility.PostAsyncAndGetLocation(requestUrl, request.ProtectionApiToken, userData);
        var userId = Guid.Parse(location.ToString().Split('/').Last());

        return new CreateUserWithPasswordResponse(userId);
    }

    /// <inheritdoc />
    public async Task<CreateUserWithPasswordAndAttributesResponse> CreateUserWithPasswordAndAttributes(
        CreateUserWithPasswordAndAttributesRequest request
    )
    {
        var userData = new CreateUserWithPasswordAndAttributesRequestJsonData(
            request.Username,
            request.Password,
            request.FirstName,
            request.LastName,
            request.Email,
            request.Enabled,
            request.Attributes,
            request.ShouldUpdatePassword
        );

        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users";
        var location = await HttpClientUtility.PostAsyncAndGetLocation(requestUrl, request.ProtectionApiToken, userData);
        var userId = Guid.Parse(location.ToString().Split('/').Last());

        return new CreateUserWithPasswordAndAttributesResponse(userId);
    }

    /// <inheritdoc />
    public async Task DeleteUser(DeleteUserRequest request)
    {
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users/{request.Id}";
        await HttpClientUtility.DeleteAsync(requestUrl, request.ProtectionApiToken);
    }

    /// <inheritdoc />
    public async Task JoinUserToGroups(JoinUserToGroupsRequest request)
    {
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/groups";
        var groups = await HttpClientUtility.GetAsync<GroupRepresentation[]>(requestUrl, request.ProtectionApiToken);

        var tasks = request.Groups.Select(async groupName =>
        {
            var group = groups.Single(x => x.Name == groupName);
            var joinGroupRequestUrl =
                $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users/{request.UserId}/groups/{group.Id}";
            await HttpClientUtility.PutAsync(joinGroupRequestUrl, request.ProtectionApiToken, null);
        });

        await Task.WhenAll(tasks);
    }

    /// <inheritdoc />
    public async Task JoinUserToClientRoles(UserRolesRequest request)
    {
        var client = await GetClientById(request.EndpointAddress, request.RealmName, request.ProtectionApiToken, request.ClientId);

        var clientRolesRequestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/clients/{client.Id}/roles";
        var clientRoles = await HttpClientUtility.GetAsync<RoleRepresentation[]>(clientRolesRequestUrl, request.ProtectionApiToken);

        var userRequestUrl =
            $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users?username={HttpUtility.UrlEncode(request.Username)}&exact=true";
        var users = await HttpClientUtility.GetAsync<UserRepresentation[]>(userRequestUrl, request.ProtectionApiToken);
        var user = users.First(x => x.UserName == request.Username);

        var joinRolesRequestUrl =
            $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users/{user.Id}/role-mappings/clients/{client.Id}";
        await HttpClientUtility.PostAsyncAndGetLocation(
            joinRolesRequestUrl,
            request.ProtectionApiToken,
            request.Roles.Select(roleName => clientRoles.Single(x => x.Name == roleName)).ToArray()
        );
    }

    /// <inheritdoc />
    public async Task JoinUserToRealmRoles(UserRolesRequest request)
    {
        var realmRolesRequestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/roles";
        var realmRoles = await HttpClientUtility.GetAsync<RoleRepresentation[]>(realmRolesRequestUrl, request.ProtectionApiToken);

        var userRequestUrl =
            $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users?username={HttpUtility.UrlEncode(request.Username)}&exact=true";
        var users = await HttpClientUtility.GetAsync<UserRepresentation[]>(userRequestUrl, request.ProtectionApiToken);
        var user = users.First(x => x.UserName == request.Username);

        var joinRolesRequestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users/{user.Id}/role-mappings/realm";
        await HttpClientUtility.PostAsyncAndGetLocation(
            joinRolesRequestUrl,
            request.ProtectionApiToken,
            request.Roles.Select(roleName => realmRoles.Single(x => x.Name == roleName)).ToArray()
        );
    }

    /// <inheritdoc />
    public async Task RemoveUserFromClientRoles(UserRolesRequest request)
    {
        var client = await GetClientById(request.EndpointAddress, request.RealmName, request.ProtectionApiToken, request.ClientId);

        var clientRolesRequestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/clients/{client.Id}/roles";
        var clientRoles = await HttpClientUtility.GetAsync<RoleRepresentation[]>(clientRolesRequestUrl, request.ProtectionApiToken);

        var userRequestUrl =
            $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users?username={HttpUtility.UrlEncode(request.Username)}&exact=true";
        var users = await HttpClientUtility.GetAsync<UserRepresentation[]>(userRequestUrl, request.ProtectionApiToken);
        var user = users.First(x => x.UserName == request.Username);

        var removeRolesRequestUrl =
            $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users/{user.Id}/role-mappings/clients/{client.Id}";
        await HttpClientUtility.DeleteAsync(
            removeRolesRequestUrl,
            request.ProtectionApiToken,
            request.Roles.Select(roleName => clientRoles.Single(x => x.Name == roleName)).ToArray()
        );
    }

    /// <inheritdoc />
    public async Task RemoveUserFromRealmRoles(UserRolesRequest request)
    {
        var realmRolesRequestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/roles";
        var realmRoles = await HttpClientUtility.GetAsync<RoleRepresentation[]>(realmRolesRequestUrl, request.ProtectionApiToken);

        var userRequestUrl =
            $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users?username={HttpUtility.UrlEncode(request.Username)}&exact=true";
        var users = await HttpClientUtility.GetAsync<UserRepresentation[]>(userRequestUrl, request.ProtectionApiToken);
        var user = users.First(x => x.UserName == request.Username);

        var joinRolesRequestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users/{user.Id}/role-mappings/realm";
        await HttpClientUtility.DeleteAsync(
            joinRolesRequestUrl,
            request.ProtectionApiToken,
            request.Roles.Select(roleName => realmRoles.Single(x => x.Name == roleName)).ToArray()
        );
    }

    /// <inheritdoc />
    public async Task InitializeUser(InitializeUserRequest request)
    {
        var passwordData = new InitializeUserRequestJsonData(request.Password, request.ShouldUpdatePassword);
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users/{request.UserId}";

        await HttpClientUtility.PutAsync(requestUrl, request.ProtectionApiToken, passwordData);
    }

    /// <inheritdoc />
    public async Task UpdateUser(UpdateUserRequest request)
    {
        var userData = new UpdateUserRequestJsonData(
            request.FirstName,
            request.LastName,
            request.Email,
            request.IsEnabled,
            request.Username
        );
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users/{request.UserId}";

        await HttpClientUtility.PutAsync(requestUrl, request.ProtectionApiToken, userData);
    }

    /// <inheritdoc />
    public async Task UpdateUserAttributes(UpdateUserAttributesRequest request)
    {
        var userData = new UpdateUserAttributesRequestJsonData(request.Attributes);
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users/{request.UserId}";

        await HttpClientUtility.PutAsync(requestUrl, request.ProtectionApiToken, userData);
    }

    /// <inheritdoc />
    public async Task VerifyUserEmail(VerifyUserEmailRequest request)
    {
        var userEmailData = new VerifyUserEmailRequestJsonData(request.IsEmailVerified);
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users/{request.UserId}";

        await HttpClientUtility.PutAsync(requestUrl, request.ProtectionApiToken, userEmailData);
    }

    /// <inheritdoc />
    public async Task<GetUserResponse?> GetUserByUsername(GetUserByUsernameRequest request)
    {
        var requestUrl =
            $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users?username={HttpUtility.UrlEncode(request.Username)}&exact=true";
        var users = await HttpClientUtility.GetAsync<List<UserRepresentation>>(requestUrl, request.ProtectionApiToken);
        var user = users.FirstOrDefault();

        return user == null
            ? null
            : new GetUserResponse(user.Id, user.FirstName, user.LastName, user.Email, user.UserName, user.Enabled, user.EmailVerified);
    }

    /// <inheritdoc />
    public async Task<GetUserResponse> GetUserById(GetUserByIdRequest request)
    {
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users/{request.UserId}";
        var user = await HttpClientUtility.GetAsync<UserRepresentation>(requestUrl, request.ProtectionApiToken);

        return new GetUserResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.UserName,
            user.Enabled,
            user.EmailVerified,
            user.Attributes
        );
    }

    /// <inheritdoc />
    public async Task<GetUserResponse?> GetUserByAttribute(GetUserByAttributeRequest request)
    {
        var requestUrl =
            $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users?q={request.AttributeKeyValuePair.Key}:{request.AttributeKeyValuePair.Value}";
        var userList = await HttpClientUtility.GetAsync<List<UserRepresentation>>(requestUrl, request.ProtectionApiToken);

        var user = userList.FirstOrDefault();

        return user == null
            ? null
            : new GetUserResponse(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.UserName,
                user.Enabled,
                user.EmailVerified,
                user.Attributes
            );
    }

    /// <inheritdoc />
    public async Task<GetUserResponse?> GetUserByEmail(GetUserByEmailRequest request)
    {
        var requestUrl =
            $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users?email={HttpUtility.UrlEncode(request.Email)}&exact=true";
        var users = await HttpClientUtility.GetAsync<List<UserRepresentation>>(requestUrl, request.ProtectionApiToken);
        var user = users.FirstOrDefault();

        return user == null
            ? null
            : new GetUserResponse(user.Id, user.FirstName, user.LastName, user.Email, user.UserName, user.Enabled, user.EmailVerified);
    }

    /// <inheritdoc />
    public async Task<GetUsersWithRoleResponse> GetUsersWithRole(GetUsersWithRoleRequest request)
    {
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/roles/{request.Role}/users";
        var users = await HttpClientUtility.GetAsync<List<UserRepresentation>>(requestUrl, request.ProtectionApiToken);
        var usersWithRole = users
            .Select(x => new User(
                x.Id,
                x.FirstName,
                x.LastName,
                x.Email,
                x.UserName,
                x.Enabled,
                x.Groups.ToReadOnlyCollection(),
                x.Attributes
            ))
            .ToList();

        return new GetUsersWithRoleResponse(usersWithRole.ToReadOnlyCollection());
    }

    /// <inheritdoc />
    public async Task<QueryRealmMembersResponse> GetRealmMembers(GetRealmMembersRequest request)
    {
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users";
        
        var queryString = new StringBuilder();
        if (request.Limit != null)
        {
            queryString.Append($"?max={request.Limit}");
        }

        var members = await HttpClientUtility.GetAsync<UserRepresentation[]>(requestUrl + queryString, request.ProtectionApiToken);

        var users = members.Select(x => new User(
            x.Id,
            x.FirstName,
            x.LastName,
            x.Email,
            x.UserName,
            x.Enabled,
            x.Groups.ToReadOnlyCollection()
        ));
        return new QueryRealmMembersResponse(users.ToReadOnlyCollection());
    }

    /// <inheritdoc />
    public async Task<GetRealmMembersCountResponse> GetRealmMembersCount(GetRealmMembersCountRequest request)
    {
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users/count";

        var userCount = await HttpClientUtility.GetAsync<int>(requestUrl, request.ProtectionApiToken);

        return new GetRealmMembersCountResponse(userCount);
    }

    /// <inheritdoc />
    public async Task<QueryRealmMembersResponse> QueryRealmMembers(QueryRealmMembersRequest request)
    {
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users";

        var queryString = new StringBuilder();

        if (request.Limit != null)
        {
            queryString.Append($"?max={request.Limit}");
        }
        if (request.First != null)
        {
            queryString = queryString.Append(queryString.Length > 0 ? "&" : "?");
            queryString.Append($"first={request.First}");
        }
        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            queryString = queryString.Append(queryString.Length > 0 ? "&" : "?");
            queryString.Append($"email={HttpUtility.UrlEncode(request.Email)}");
        }
        if (!string.IsNullOrWhiteSpace(request.FirstName))
        {
            queryString = queryString.Append(queryString.Length > 0 ? "&" : "?");
            queryString.Append($"firstName={HttpUtility.UrlEncode(request.FirstName)}");
        }
        if (!string.IsNullOrWhiteSpace(request.LastName))
        {
            queryString = queryString.Append(queryString.Length > 0 ? "&" : "?");
            queryString.Append($"lastName={HttpUtility.UrlEncode(request.LastName)}");
        }
        if (!string.IsNullOrWhiteSpace(request.Username))
        {
            queryString = queryString.Append(queryString.Length > 0 ? "&" : "?");
            queryString.Append($"username={HttpUtility.UrlEncode(request.Username)}");
        }
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryString = queryString.Append(queryString.Length > 0 ? "&" : "?");
            queryString.Append($"search={HttpUtility.UrlEncode(request.Search)}");
        }

        var members = await HttpClientUtility.GetAsync<UserRepresentation[]>(requestUrl + queryString, request.ProtectionApiToken);
        var users = members.Select(x => new User(
            x.Id,
            x.FirstName,
            x.LastName,
            x.Email,
            x.UserName,
            x.Enabled,
            x.Groups.ToReadOnlyCollection(),
            x.Attributes
        ));
        return new QueryRealmMembersResponse(users.ToReadOnlyCollection());
    }

    /// <inheritdoc />
    public async Task<GetUserRolesResponse> GetUserClientRoles(GetUserRolesRequest request)
    {
        var client = await GetClientById(request.EndpointAddress, request.RealmName, request.ProtectionApiToken, request.ClientId!);

        var userRequestUrl =
            $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users?username={HttpUtility.UrlEncode(request.Username)}&exact=true";
        var users = await HttpClientUtility.GetAsync<UserRepresentation[]>(userRequestUrl, request.ProtectionApiToken);

        var roles = new List<Role>();
        if (users.Length != 0)
        {
            var requestUrl =
                $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users/{users[0].Id}/role-mappings/clients/{client.Id}";
            var clientRolesRepresentation = await HttpClientUtility.GetAsync<RoleRepresentation[]>(requestUrl, request.ProtectionApiToken);

            roles = clientRolesRepresentation.Select(x => new Role(x.Id, x.Name, x.Description)).ToList();
        }

        return new GetUserRolesResponse(roles.ToReadOnlyCollection());
    }

    /// <inheritdoc />
    public async Task<GetUserRolesResponse> GetUserRealmRoles(GetUserRolesRequest request)
    {
        var userRequestUrl =
            $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users?username={HttpUtility.UrlEncode(request.Username)}&exact=true";
        var users = await HttpClientUtility.GetAsync<UserRepresentation[]>(userRequestUrl, request.ProtectionApiToken);

        var roles = new List<Role>();
        if (users.Length != 0)
        {
            var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users/{users[0].Id}/role-mappings";
            var realmRolesRepresentation = await HttpClientUtility.GetAsync<RealmRoleRepresentation>(
                requestUrl,
                request.ProtectionApiToken
            );

            roles = realmRolesRepresentation.RoleRepresentations.Select(x => new Role(x.Id, x.Name, x.Description)).ToList();
        }

        return new GetUserRolesResponse(roles.ToReadOnlyCollection());
    }

    /// <inheritdoc />
    public async Task ForgotPassword(ForgotPasswordRequest request)
    {
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users/{request.UserId}/execute-actions-email";

        await HttpClientUtility.PutAsync(requestUrl, request.ProtectionApiToken, request.RequiredActions);
    }

    /// <inheritdoc />
    public async Task SendVerifyEmail(SendVerifyEmailRequest request)
    {
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users/{request.UserId}/send-verify-email";

        await HttpClientUtility.PutAsync(requestUrl, request.ProtectionApiToken, string.Empty);
    }

    /// <inheritdoc />
    public async Task ChangePassword(ChangePasswordRequest request)
    {
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users/{request.UserId}/reset-password";
        var passwordData = new ChangePasswordRequestJsonData(request.Password);
        await HttpClientUtility.PutAsync(requestUrl, request.ProtectionApiToken, passwordData);
    }

    /// <inheritdoc />
    public async Task<GetUserSessionsResponse> GetUserSessions(GetUserSessionsRequest request)
    {
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users/{request.UserId}/sessions";

        var sessions = await HttpClientUtility.GetAsync<UserSession[]>(requestUrl, request.ProtectionApiToken);

        return new GetUserSessionsResponse(sessions);
    }

    /// <inheritdoc />
    public async Task<GetUserClientSessionsResponse> GetUserClientSessions(GetUserClientSessionsRequest request)
    {
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/users/{request.UserId}/sessions";

        var sessions = await HttpClientUtility.GetAsync<UserSession[]>(requestUrl, request.ProtectionApiToken);

        var clientSessions = sessions.Where(s => s.Clients.ContainsValue(request.ClientId)).OrderByDescending(s => s.Start);

        return new GetUserClientSessionsResponse(clientSessions.ToReadOnlyCollection());
    }
}
