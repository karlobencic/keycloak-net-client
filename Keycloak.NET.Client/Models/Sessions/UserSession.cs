namespace NextLevelDev.Keycloak.Models.Sessions;

public sealed record UserSession(
    string Id,
    string Username,
    string UserId,
    string IpAddress,
    long Start,
    long LastAccess,
    Dictionary<string, string> Clients
)
{
    /// <summary>
    /// Epoch
    /// </summary>
    public long Start { get; } = Start;

    /// <summary>
    /// Epoch
    /// </summary>
    public long LastAccess { get; } = LastAccess;

    /// <summary>
    /// Key = client ID (not client-id)
    /// Value = client-id (name)
    /// </summary>
    public Dictionary<string, string> Clients { get; } = Clients;
}
