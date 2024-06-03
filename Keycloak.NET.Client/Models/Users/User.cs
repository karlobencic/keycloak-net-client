namespace NextLevelDev.Keycloak.Models.Users;

public sealed record User(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Username,
    bool IsEnabled,
    IReadOnlyCollection<Guid> Groups
)
{
    public Dictionary<string, string[]> Attributes { get; }

    public User(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string username,
        bool isEnabled,
        IReadOnlyCollection<Guid> groups,
        Dictionary<string, string[]> attributes
    )
        : this(id, firstName, lastName, email, username, isEnabled, groups)
    {
        Attributes = attributes;
    }
}
