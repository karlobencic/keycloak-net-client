using NextLevelDev.Keycloak.Common;

namespace NextLevelDev.Keycloak.Models.Login;

public sealed record LoginRequest : KeycloakRequestBase
{
    public string Protocol { get; }
    public string ClientId { get; }
    public string? ClientSecret { get; }
    public string GrantType { get; }
    public string? Scope { get; }
    public string? Username { get; }
    public string? Password { get; }
    public string? RefreshToken { get; }

    public LoginRequest(
        string endpointAddress,
        string realmName,
        string protocol,
        string clientId,
        string clientSecret,
        string scope,
        string username,
        string password
    )
        : base(endpointAddress, realmName)
    {
        Protocol = protocol;
        ClientId = clientId;
        ClientSecret = clientSecret;
        Scope = scope;
        Username = username;
        Password = password;
        GrantType = "password";
    }

    public LoginRequest(
        string endpointAddress,
        string realmName,
        string protocol,
        string refreshToken,
        string clientId,
        string? clientSecret = null
    )
        : base(endpointAddress, realmName)
    {
        Protocol = protocol;
        ClientId = clientId;
        RefreshToken = refreshToken;
        GrantType = "refresh_token";
        ClientSecret = clientSecret;
    }
}
