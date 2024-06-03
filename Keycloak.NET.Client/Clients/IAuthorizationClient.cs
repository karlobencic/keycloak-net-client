using NextLevelDev.Keycloak.Models.PermissionTicket;
using NextLevelDev.Keycloak.Models.RequestingPartyToken;
using NextLevelDev.Keycloak.Models.Resources;

namespace NextLevelDev.Keycloak.Clients;

public interface IAuthorizationClient : IKeycloakBaseClient
{
    /// <summary>
    /// Gets Permission Ticket. Permission ticket is used to define action scope against defined resource that is being requested.
    /// </summary>
    /// <param name="request">Action scope against defined resource</param>
    /// <returns>Permission ticket</returns>
    Task<GetPermissionTicketResponse> GetPermissionTicket(GetPermissionTicketRequest request);

    /// <summary>
    /// Gets resources ids for given client
    /// </summary>
    /// <param name="request">Client metadata</param>
    /// <returns>List of resources ids</returns>
    Task<string[]> GetResources(GetResourcesRequest request);

    /// <summary>
    /// Evaluates requested permission (resource-scope) for given user against authorization service. Method evaluates Requesting Party Token (RPT).
    /// RPT tells whether user is authorized for accessing resource using defined scope which was defined in permission ticket.
    /// </summary>
    /// <param name="request">Request contains permission ticket for one exact resource and one exact scope</param>
    /// <returns>Evaluation result. True if authorized.</returns>
    Task<EvaluatePermissionResponse> EvaluatePermission(EvaluatePermissionsRequest request);

    /// <summary>
    /// Evaluates requested permissions (resource-scope) for given user against authorization service. Method evaluates Requesting Party Token (RPT).
    /// RPT tells whether user is authorized for accessing resource using defined scope (optional) which was defined in permission ticket.
    /// </summary>
    /// <param name="request">Request contains permission ticket for multiple resources</param>
    /// <returns>Evaluation result. Contains list of resources and scopes inside those resources for which user is authorized.</returns>
    Task<EvaluatePermissionsResponse> EvaluatePermissions(EvaluatePermissionsRequest request);
}
