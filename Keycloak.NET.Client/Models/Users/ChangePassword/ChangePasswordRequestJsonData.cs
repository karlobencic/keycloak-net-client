using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.Users.ChangePassword;

internal sealed record ChangePasswordRequestJsonData(string Value)
{
    [JsonPropertyName("type")]
    public string Type => "password";

    [JsonPropertyName("value")]
    public string Value { get; private set; } = Value;
}
