using NextLevelDev.Keycloak.Models.Groups;

namespace NextLevelDev.Keycloak.Clients;

public interface IGroupsClient : IKeycloakBaseClient
{
    /// <summary>
    /// Returns users for selected realm and group.
    /// </summary>
    Task<GetGroupMembersResponse> GetGroupMembers(GetGroupMembersRequest request);
}
