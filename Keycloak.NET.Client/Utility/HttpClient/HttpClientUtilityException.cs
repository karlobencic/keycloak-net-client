using System.Net;

namespace NextLevelDev.Keycloak.Utility.HttpClient;

/// <summary>
/// Exception that contains http status code received from HttpRequestException. This exception is used because there is no other way to preserve
/// http status code.
/// </summary>
public class HttpClientUtilityException(string message, HttpStatusCode statusCode) : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;

    public string? ErrorResponse { get; }

    public string? ErrorDescription { get; }

    public HttpClientUtilityException(string message, HttpStatusCode statusCode, string? errorResponse, string? errorDescription)
        : this(message, statusCode)
    {
        ErrorResponse = errorResponse;
        ErrorDescription = errorDescription;
    }
}
