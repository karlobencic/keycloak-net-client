using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Users.Update;

public sealed record UpdateUserRequest(
    string EndpointAddress,
    string RealmName,
    string ProtectionApiToken,
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    bool IsEnabled,
    string? Username = null
) : KeycloakRequestBase(EndpointAddress, RealmName)
{
    /// <summary>
    /// It will be ignored if null
    /// </summary>
    public string? Username { get; } = Username;
}
