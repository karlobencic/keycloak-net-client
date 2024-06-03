using System.Text;
using NextLevelDev.Keycloak.Models.Groups;
using NextLevelDev.Keycloak.Models.Users;
using NextLevelDev.Keycloak.Utility.Extensions;
using NextLevelDev.Keycloak.Utility.HttpClient;

namespace NextLevelDev.Keycloak.Clients;

internal class GroupsClient(IHttpClientUtility httpClientUtility) : KeycloakBaseClient(httpClientUtility), IGroupsClient
{
    /// <inheritdoc />
    public async Task<GetGroupMembersResponse> GetGroupMembers(GetGroupMembersRequest request)
    {
        //get all groups
        var getGroupsUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/groups";
        var groups = await HttpClientUtility.GetAsync<GroupRepresentation[]>(getGroupsUrl, request.ProtectionApiToken);
        var group = groups.Single(x => x.Name == request.GroupName);

        string groupId =
            group.SubGroups.Length == 0 ? group.Id : group.SubGroups.FirstOrDefault(x => x.Name == request.SubGroupName)?.Id ?? group.Id;

        //get group members
        var getGroupMembersUrl = $"{request.EndpointAddress}/admin/realms/{request.RealmName}/groups/{groupId}/members";

        var queryString = new StringBuilder();
        if (request.Limit != null)
        {
            queryString.Append($"?max={request.Limit}");
        }

        var members = await HttpClientUtility.GetAsync<UserRepresentation[]>(getGroupMembersUrl + queryString, request.ProtectionApiToken);

        var users = members.Select(x => new User(
            x.Id,
            x.FirstName,
            x.LastName,
            x.Email,
            x.UserName,
            x.Enabled,
            x.Groups.ToReadOnlyCollection()
        ));
        return new GetGroupMembersResponse(users.ToReadOnlyCollection());
    }
}
