using System.Collections.ObjectModel;

namespace NextLevelDev.Keycloak.Models.RequestingPartyToken;

public sealed record EvaluatePermissionsResponse
{
    public IReadOnlyCollection<ResourcePermission> Permissions { get; }

    public EvaluatePermissionsResponse(IList<ResourcePermission> permissions)
    {
        Permissions = new ReadOnlyCollection<ResourcePermission>(permissions);
    }
}
