using System.Text.Json.Serialization;
using NextLevelDev.Keycloak.Models.Users.Initialize;

namespace NextLevelDev.Keycloak.Models.Users.Create;

internal sealed record CreateUserWithPasswordRequestJsonData
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

    public CreateUserWithPasswordRequestJsonData(
        string username,
        string password,
        string firstName,
        string lastName,
        string email,
        bool enabled,
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
        RequiredActions = shouldUpdatePassword ? ["VERIFY_EMAIL", "UPDATE_PASSWORD"] : ["VERIFY_EMAIL"];
    }
}
