namespace NextLevelDev.Keycloak.Models.Users.Get;

public sealed record GetUserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Username,
    bool IsEnabled,
    bool IsEmailVerified
)
{
    public Dictionary<string, string[]> Attributes { get; }

    public GetUserResponse(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string username,
        bool isEnabled,
        bool isEmailVerified,
        Dictionary<string, string[]> attributes
    )
        : this(id, firstName, lastName, email, username, isEnabled, isEmailVerified)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Username = username;
        IsEnabled = isEnabled;
        IsEmailVerified = isEmailVerified;
        Attributes = attributes;
    }
}
