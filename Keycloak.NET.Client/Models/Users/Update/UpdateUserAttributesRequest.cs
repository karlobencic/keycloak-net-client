using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Users.Update;

public sealed record UpdateUserAttributesRequest(
    string EndpointAddress,
    string RealmName,
    string ProtectionApiToken,
    Guid UserId,
    Dictionary<string, string> Attributes
) : KeycloakRequestBase(EndpointAddress, RealmName);
