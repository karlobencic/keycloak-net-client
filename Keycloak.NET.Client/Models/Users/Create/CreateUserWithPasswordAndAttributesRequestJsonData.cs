using System.Text.Json.Serialization;
using NextLevelDev.Keycloak.Models.Users.Initialize;

namespace NextLevelDev.Keycloak.Models.Users.Create;

internal sealed record CreateUserWithPasswordAndAttributesRequestJsonData
{
    [JsonPropertyName("username")]
    public string Username { get; }

    [JsonPropertyName("credentials")]
    public UserCredentialJsonData[] Credentials { get; }

    [JsonPropertyName("firstName")]
    public string FirstName { get; }

    [JsonPropertyName("lastName")]
    public string LastName { get; }

    [JsonPropertyName("email")]
    public string Email { get; }

    [JsonPropertyName("emailVerified")]
    public bool EmailVerified { get; }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; }

    [JsonPropertyName("requiredActions")]
    public string[] RequiredActions { get; }

    [JsonPropertyName("attributes")]
    public Dictionary<string, string> Attributes { get; }

    public CreateUserWithPasswordAndAttributesRequestJsonData(
        string username,
        string password,
        string firstName,
        string lastName,
        string email,
        bool enabled,
        Dictionary<string, string> attributes,
        bool shouldUpdatePassword
    )
    {
        Username = username;
        Credentials = [new UserCredentialJsonData("password", password)];
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        EmailVerified = false;
        Enabled = enabled;
        Attributes = attributes;
        RequiredActions = shouldUpdatePassword ? ["VERIFY_EMAIL", "UPDATE_PASSWORD"] : Array.Empty<string>();
    }
}
