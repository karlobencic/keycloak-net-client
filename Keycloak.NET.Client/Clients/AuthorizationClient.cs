using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text.Json;
using NextLevelDev.Keycloak.Models.PermissionTicket;
using NextLevelDev.Keycloak.Models.RequestingPartyToken;
using NextLevelDev.Keycloak.Models.Resources;
using NextLevelDev.Keycloak.Utility.HttpClient;

namespace NextLevelDev.Keycloak.Clients;

internal class AuthorizationClient(IHttpClientUtility httpClientUtility) : KeycloakBaseClient(httpClientUtility), IAuthorizationClient
{
    /// <inheritdoc />
    public async Task<GetPermissionTicketResponse> GetPermissionTicket(GetPermissionTicketRequest request)
    {
        var jsonData = request.ResourceIds.Select(x => new GetPermissionTicketRequestJsonData(x)).ToArray();

        var requestUrl = $"{request.EndpointAddress}/realms/{request.RealmName}/authz/protection/permission";
        var permissionTicket = await HttpClientUtility.PostAsync<GetPermissionTicketResponseJsonData>(
            requestUrl,
            request.ProtectionApiToken,
            jsonData
        );

        return new GetPermissionTicketResponse(permissionTicket.Ticket);
    }

    /// <inheritdoc />
    public async Task<string[]> GetResources(GetResourcesRequest request)
    {
        var requestUrl = $"{request.EndpointAddress}/realms/{request.RealmName}/authz/protection/resource_set";
        return await HttpClientUtility.GetAsync<string[]>(requestUrl, request.ProtectionApiToken);
    }

    /// <inheritdoc />
    public async Task<EvaluatePermissionResponse> EvaluatePermission(EvaluatePermissionsRequest request)
    {
        var formData = new List<KeyValuePair<string, string>>
        {
            new("grant_type", "urn:ietf:params:oauth:grant-type:uma-ticket"),
            new("ticket", request.PermissionTicket)
        };

        bool isAuthorized;
        try
        {
            var requestUrl = $"{request.EndpointAddress}/realms/{request.RealmName}/protocol/openid-connect/token";
            var token = await HttpClientUtility.PostAsFormDataAsync<GetRequestingPartyTokenResponseJsonData>(
                requestUrl,
                request.UserAccessToken,
                formData
            );
            isAuthorized = token.AccessToken != null;
        }
        catch (HttpClientUtilityException ex)
        {
            if (ex.StatusCode == HttpStatusCode.Forbidden)
            {
                isAuthorized = false;
            }
            else
            {
                throw;
            }
        }

        return new EvaluatePermissionResponse(isAuthorized);
    }

    /// <inheritdoc />
    public async Task<EvaluatePermissionsResponse> EvaluatePermissions(EvaluatePermissionsRequest request)
    {
        var formData = new List<KeyValuePair<string, string>>
        {
            new("grant_type", "urn:ietf:params:oauth:grant-type:uma-ticket"),
            new("ticket", request.PermissionTicket)
        };

        bool isAuthorized;
        GetRequestingPartyTokenResponseJsonData? accessToken = null;
        try
        {
            var requestUrl = $"{request.EndpointAddress}/realms/{request.RealmName}/protocol/openid-connect/token";
            accessToken = await HttpClientUtility.PostAsFormDataAsync<GetRequestingPartyTokenResponseJsonData>(
                requestUrl,
                request.UserAccessToken,
                formData
            );
            isAuthorized = accessToken.AccessToken != null;
        }
        catch (HttpClientUtilityException ex)
        {
            if (ex.StatusCode == HttpStatusCode.Forbidden)
            {
                isAuthorized = false;
            }
            else
            {
                throw;
            }
        }

        if (!isAuthorized)
        {
            return new EvaluatePermissionsResponse(Enumerable.Empty<ResourcePermission>().ToList());
        }

        var jwtHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtHandler.ReadJwtToken(accessToken.AccessToken);

        var authorization = JsonSerializer.Deserialize<AuthorizationJsonData>(jwtToken.Payload["authorization"].ToString());
        var permissions = authorization
            .Permissions.Where(x => x.ResourceName != "Default Resource")
            .Select(x => new ResourcePermission(x.ResourceName, x.Scopes));

        return new EvaluatePermissionsResponse(permissions.ToList());
    }
}
