using NextLevelDev.Keycloak.Models.Client;

namespace NextLevelDev.Keycloak.Clients;

public interface IKeycloakBaseClient
{
    Task<ClientRepresentation> GetClientById(string endpointAddress, string realmName, string protectionApiToken, string clientId);
}
