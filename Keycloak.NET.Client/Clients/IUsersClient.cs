using NextLevelDev.Keycloak.Models.Groups;
using NextLevelDev.Keycloak.Models.Roles;
using NextLevelDev.Keycloak.Models.Sessions;
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

namespace NextLevelDev.Keycloak.Clients;

public interface IUsersClient : IKeycloakBaseClient
{
    /// <summary>
    /// Creates user
    /// </summary>
    Task<CreateUserResponse> CreateUser(CreateUserRequest request);

    /// <summary>
    /// Creates user with password
    /// </summary>
    Task<CreateUserWithPasswordResponse> CreateUserWithPassword(CreateUserWithPasswordRequest request);

    /// <summary>
    /// Creates user with password and attributes
    /// </summary>
    Task<CreateUserWithPasswordAndAttributesResponse> CreateUserWithPasswordAndAttributes(
        CreateUserWithPasswordAndAttributesRequest request
    );

    /// <summary>
    /// Deletes user
    /// </summary>
    Task DeleteUser(DeleteUserRequest request);

    /// <summary>
    /// Joins user to given groups
    /// </summary>
    Task JoinUserToGroups(JoinUserToGroupsRequest request);

    /// <summary>
    /// Joins user to given client roles
    /// </summary>
    Task JoinUserToClientRoles(UserRolesRequest request);

    /// <summary>
    /// Joins user to given realm roles
    /// </summary>
    Task JoinUserToRealmRoles(UserRolesRequest request);

    /// <summary>
    /// Removes user from given client roles
    /// </summary>
    Task RemoveUserFromClientRoles(UserRolesRequest request);

    /// <summary>
    /// Removes user from given realm roles
    /// </summary>
    Task RemoveUserFromRealmRoles(UserRolesRequest request);

    /// <summary>
    /// Sets user's account for the first time
    /// </summary>
    Task InitializeUser(InitializeUserRequest request);

    /// <summary>
    /// Updates user data
    /// </summary>
    Task UpdateUser(UpdateUserRequest request);

    /// <summary>
    /// Updates user attributes
    /// </summary>
    Task UpdateUserAttributes(UpdateUserAttributesRequest request);

    /// <summary>
    /// Verify user email
    /// </summary>
    Task VerifyUserEmail(VerifyUserEmailRequest request);

    /// <summary>
    /// Gets user by username
    /// </summary>
    Task<GetUserResponse?> GetUserByUsername(GetUserByUsernameRequest request);

    /// <summary>
    /// Gets user by id
    /// </summary>
    Task<GetUserResponse> GetUserById(GetUserByIdRequest request);

    /// <summary>
    /// Gets user by specified attribute
    /// </summary>
    Task<GetUserResponse?> GetUserByAttribute(GetUserByAttributeRequest request);

    /// <summary>
    /// Gets user by email
    /// </summary>
    Task<GetUserResponse?> GetUserByEmail(GetUserByEmailRequest request);

    /// <summary>
    /// Returns list of realm users for given role
    /// </summary>
    Task<GetUsersWithRoleResponse> GetUsersWithRole(GetUsersWithRoleRequest request);

    /// <summary>
    /// Returns users for selected realm.
    /// </summary>
    Task<QueryRealmMembersResponse> GetRealmMembers(GetRealmMembersRequest request);

    /// <summary>
    /// Returns the count of users for selected realm.
    /// </summary>
    Task<GetRealmMembersCountResponse> GetRealmMembersCount(GetRealmMembersCountRequest request);

    /// <summary>
    /// Returns users for selected realm filtered by query parameters.
    /// </summary>
    Task<QueryRealmMembersResponse> QueryRealmMembers(QueryRealmMembersRequest request);

    /// <summary>
    /// Returns list of client roles for given user
    /// </summary>
    Task<GetUserRolesResponse> GetUserClientRoles(GetUserRolesRequest request);

    /// <summary>
    /// Returns list of realm roles for given user
    /// </summary>
    Task<GetUserRolesResponse> GetUserRealmRoles(GetUserRolesRequest request);

    /// <summary>
    /// Send reset password email to user
    /// </summary>
    Task ForgotPassword(ForgotPasswordRequest request);

    /// <summary>
    /// Send verification email to user
    /// </summary>
    Task SendVerifyEmail(SendVerifyEmailRequest request);

    /// <summary>
    /// Change user password
    /// </summary>
    Task ChangePassword(ChangePasswordRequest request);

    /// <summary>
    /// Gets all user sessions for a specific realm
    /// </summary>
    Task<GetUserSessionsResponse> GetUserSessions(GetUserSessionsRequest request);

    /// <summary>
    /// Gets all user sessions for a specific realm and client
    /// </summary>
    Task<GetUserClientSessionsResponse> GetUserClientSessions(GetUserClientSessionsRequest request);
}
