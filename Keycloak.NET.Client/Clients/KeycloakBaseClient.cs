using NextLevelDev.Keycloak.Models.Client;
using NextLevelDev.Keycloak.Utility.HttpClient;

namespace NextLevelDev.Keycloak.Clients;

internal abstract class KeycloakBaseClient(IHttpClientUtility httpClientUtility) : IKeycloakBaseClient
{
    protected readonly IHttpClientUtility HttpClientUtility = httpClientUtility;

    public async Task<ClientRepresentation> GetClientById(
        string endpointAddress,
        string realmName,
        string protectionApiToken,
        string clientId
    )
    {
        var requestUrl = $"{endpointAddress}/admin/realms/{realmName}/clients";
        var clients = await HttpClientUtility.GetAsync<ClientRepresentation[]>(requestUrl, protectionApiToken);

        return clients.Single(x => x.ClientId == clientId);
    }
}
