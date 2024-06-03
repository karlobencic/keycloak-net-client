using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Users.Get;

public sealed record GetUserByAttributeRequest(
    string EndpointAddress,
    string RealmName,
    string ProtectionApiToken,
    KeyValuePair<string, string> AttributeKeyValuePair
) : KeycloakRequestBase(EndpointAddress, RealmName);
