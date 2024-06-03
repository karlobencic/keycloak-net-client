namespace NextLevelDev.Keycloak.Models.Sessions;

public sealed record GetUserSessionsResponse(IReadOnlyCollection<UserSession> UserSessions);
