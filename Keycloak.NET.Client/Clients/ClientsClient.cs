using NextLevelDev.Keycloak.Models.Client;
using NextLevelDev.Keycloak.Utility.HttpClient;

namespace NextLevelDev.Keycloak.Clients;

internal class ClientsClient(IHttpClientUtility httpClientUtility) : KeycloakBaseClient(httpClientUtility), IClientsClient
{
    /// <inheritdoc />
    public async Task<GetClientResponse> GetClient(GetClientRequest request)
    {
        var client = await GetClientById(request.EndpointAddress, request.RealmName, request.ProtectionApiToken, request.ClientId);
        return new GetClientResponse(client.Id, client.ClientId);
    }
}
