using NextLevelDev.Keycloak.Models.Client;

namespace NextLevelDev.Keycloak.Clients;

public interface IClientsClient : IKeycloakBaseClient
{
    /// <summary>
    /// Returns client by client id (name not id)
    /// </summary>
    Task<GetClientResponse> GetClient(GetClientRequest request);
}
