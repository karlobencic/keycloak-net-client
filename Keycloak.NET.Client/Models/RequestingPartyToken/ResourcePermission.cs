namespace NextLevelDev.Keycloak.Models.RequestingPartyToken;

public sealed record ResourcePermission(string ResourceName, string[] Scopes);
