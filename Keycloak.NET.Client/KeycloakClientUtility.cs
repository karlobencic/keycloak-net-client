using NextLevelDev.Keycloak.Clients;
using NextLevelDev.Keycloak.Models.Login;
using NextLevelDev.Keycloak.Models.Logout;
using NextLevelDev.Keycloak.Models.ProtectionApiToken;
using NextLevelDev.Keycloak.Models.Sessions;
using NextLevelDev.Keycloak.Utility.HttpClient;

namespace NextLevelDev.Keycloak;

public class KeycloakClientUtility : IKeycloakClientUtility
{
    private readonly IHttpClientUtility _httpClientUtility;

    public KeycloakClientUtility(IHttpClientFactory httpClientFactory)
    {
        _httpClientUtility = new HttpClientUtility(httpClientFactory);
    }

    public IAuthorizationClient Authorization => new AuthorizationClient(_httpClientUtility);
    public IUsersClient Users => new UsersClient(_httpClientUtility);
    public IRolesClient Roles => new RolesClient(_httpClientUtility);
    public IGroupsClient Groups => new GroupsClient(_httpClientUtility);
    public IClientsClient Clients => new ClientsClient(_httpClientUtility);

    /// <inheritdoc />
    public async Task<GetProtectionApiTokenResponse> GetProtectionApiToken(GetProtectionApiTokenRequest request)
    {
        var formData = new List<KeyValuePair<string, string>> { new("grant_type", "client_credentials") };

        if (!string.IsNullOrEmpty(request.Scope))
        {
            formData.Add(new KeyValuePair<string, string>("scope", request.Scope));
        }

        var requestUrl = $"{request.EndpointAddress}/realms/{request.RealmName}/protocol/openid-connect/token";
        var token = await _httpClientUtility.PostAsFormDataAsync<GetProtectionApiTokenResponseJsonData>(
            requestUrl,
            request.ClientId,
            request.ClientSecret,
            formData
        );

        return new GetProtectionApiTokenResponse(token.AccessToken, token.ExpiresIn, token.TokenType, token.Scope);
    }

    /// <inheritdoc />
    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var requestUrl = $"{request.EndpointAddress}/realms/{request.RealmName}/protocol/{request.Protocol}/token";

        var formData = new List<KeyValuePair<string, string>> { new("grant_type", request.GrantType), new("client_id", request.ClientId) };

        if (!string.IsNullOrEmpty(request.ClientSecret))
        {
            formData.Add(new KeyValuePair<string, string>("client_secret", request.ClientSecret));
        }

        switch (request.GrantType)
        {
            case "password":
                formData.Add(new KeyValuePair<string, string>("scope", request.Scope!));
                formData.Add(new KeyValuePair<string, string>("username", request.Username!));
                formData.Add(new KeyValuePair<string, string>("password", request.Password!));
                break;
            case "refresh_token":
                formData.Add(new KeyValuePair<string, string>("refresh_token", request.RefreshToken!));
                break;
        }

        var response = await _httpClientUtility.PostAsFormDataAsync<LoginResponseJsonData>(requestUrl, null, formData);
        return new LoginResponse(
            response.AccessToken,
            response.RefreshToken,
            response.AccessTokenExpireSeconds,
            response.RefreshTokenExpireSeconds,
            response.TokenType,
            response.NotBeforePolicy,
            response.SessionState,
            response.Scope
        );
    }

    /// <inheritdoc />
    public async Task Logout(LogoutRequest request)
    {
        var requestUrl = $"{request.EndpointAddress}/realms/{request.RealmName}/protocol/{request.Protocol}/logout";

        var formData = new List<KeyValuePair<string, string>>
        {
            new("refresh_token", request.RefreshToken),
            new("client_id", request.ClientId),
            new("client_secret", request.ClientSecret)
        };

        await _httpClientUtility.PostAsFormDataAsync<LogoutResponse>(requestUrl, request.AccessToken, formData);
    }

    /// <inheritdoc />
    public async Task DeleteSession(DeleteSessionRequest request)
    {
        var requestUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/sessions/{request.SessionId}";

        await _httpClientUtility.DeleteAsync(requestUrl, request.ProtectionApiToken);
    }
}
