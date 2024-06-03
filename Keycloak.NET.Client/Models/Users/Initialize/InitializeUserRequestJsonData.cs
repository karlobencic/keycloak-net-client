using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.Users.Initialize;

internal sealed record InitializeUserRequestJsonData
{
    [JsonPropertyName("credentials")]
    public UserCredentialJsonData[] Credentials { get; }

    [JsonPropertyName("emailVerified")]
    public bool EmailVerified { get; }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; }

    [JsonPropertyName("requiredActions")]
    public string[] RequiredActions { get; }

    public InitializeUserRequestJsonData(string password, bool shouldUpdatePassword)
    {
        Credentials = [new UserCredentialJsonData("password", password)];
        EmailVerified = true;
        Enabled = true;

        RequiredActions = shouldUpdatePassword ? ["UPDATE_PASSWORD"] : Array.Empty<string>();
    }
}
