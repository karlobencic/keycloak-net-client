namespace NextLevelDev.Keycloak.Models.Sessions;

public sealed record GetUserClientSessionsResponse(IReadOnlyCollection<UserSession> UserSessions);
