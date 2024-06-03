using NextLevelDev.Keycloak.Clients;
using NextLevelDev.Keycloak.Models.Login;
using NextLevelDev.Keycloak.Models.Logout;
using NextLevelDev.Keycloak.Models.ProtectionApiToken;
using NextLevelDev.Keycloak.Models.Sessions;

namespace NextLevelDev.Keycloak;

/// <summary>
/// Defines methods for communication with Keycloak IAM
/// </summary>
public interface IKeycloakClientUtility
{
    /// <summary>
    /// Gets client for communication with Keycloak Authz API
    /// </summary>
    IAuthorizationClient Authorization { get; }
    
    /// <summary>
    /// Gets client for communication with Keycloak Users API
    /// </summary>
    IUsersClient Users { get; }

    /// <summary>
    /// Gets client for communication with Keycloak Roles API
    /// </summary>
    IRolesClient Roles { get; }

    /// <summary>
    /// Gets client for communication with Keycloak Groups API
    /// </summary>
    IGroupsClient Groups { get; }

    /// <summary>
    /// Gets client for communication with Keycloak Clients API
    /// </summary>
    IClientsClient Clients { get; }

    /// <summary>
    /// Gets protection API token (PAT). PAT is used to identify client against Keycloak
    /// </summary>
    /// <param name="request">Get protection API token request</param>
    /// <returns>Protection API token</returns>
    Task<GetProtectionApiTokenResponse> GetProtectionApiToken(GetProtectionApiTokenRequest request);

    /// <summary>
    /// Login (authenticate) user by username and password
    /// </summary>
    Task<LoginResponse> Login(LoginRequest request);

    /// <summary>
    /// Logout user
    /// </summary>
    Task Logout(LogoutRequest request);

    /// <summary>
    /// Deletes a user session.
    /// This does not invalidate the access_token,
    /// it stays valid until it's expired just cannot be refreshed later on
    /// </summary>
    Task DeleteSession(DeleteSessionRequest request);
}
