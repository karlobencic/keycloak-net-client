using System.Text.Json.Serialization;

namespace NextLevelDev.Keycloak.Models.Users.VerifyEmail;

public sealed record VerifyUserEmailRequestJsonData([property: JsonPropertyName("emailVerified")] bool EmailVerified);
