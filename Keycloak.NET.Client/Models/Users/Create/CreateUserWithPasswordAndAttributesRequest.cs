using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Users.Create;

public sealed record CreateUserWithPasswordAndAttributesRequest(
    string EndpointAddress,
    string RealmName,
    string ProtectionApiToken,
    string Username,
    string Password,
    string FirstName,
    string LastName,
    string Email,
    bool Enabled,
    Dictionary<string, string> Attributes,
    bool ShouldUpdatePassword = false
) : KeycloakRequestBase(EndpointAddress, RealmName);
