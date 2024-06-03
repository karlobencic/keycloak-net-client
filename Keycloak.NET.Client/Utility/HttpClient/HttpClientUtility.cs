using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using NextLevelDev.Keycloak.Error;

namespace NextLevelDev.Keycloak.Utility.HttpClient;

public class HttpClientUtility : IHttpClientUtility
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpClientUtility(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <inheritdoc />
    public async Task<T> PostAsync<T>(string url)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.PostAsync(url, null);
        await EnsureSuccessStatusCode(responseMessage);
        return await MakeResponse<T>(responseMessage);
    }

    /// <inheritdoc />
    public async Task<T> PostAsFormDataAsync<T>(
        string url,
        string username,
        string password,
        IReadOnlyCollection<KeyValuePair<string, string>> nameValueCollection
    )
    {
        var client = _httpClientFactory.CreateClient();
        var content = new FormUrlEncodedContent(nameValueCollection);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded") { CharSet = "UTF-8" };

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(url),
            Content = content
        };

        byte[] byteArray = Encoding.UTF8.GetBytes($"{username}:{password}");
        request.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(byteArray)}");

        var responseMessage = await client.SendAsync(request);
        await EnsureSuccessStatusCode(responseMessage);

        return await MakeResponse<T>(responseMessage);
    }

    /// <inheritdoc />
    public async Task<T> PostAsFormDataAsync<T>(string url, string? bearerToken, IReadOnlyCollection<KeyValuePair<string, string>> nameValueCollection)
    {
        var client = _httpClientFactory.CreateClient();
        var content = new FormUrlEncodedContent(nameValueCollection);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded") { CharSet = "UTF-8" };
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(url),
            Content = content
        };
        
        if (!string.IsNullOrWhiteSpace(bearerToken))
        {
            request.Headers.Add("Authorization", $"Bearer {bearerToken}");
        }

        var responseMessage = await client.SendAsync(request);
        await EnsureSuccessStatusCode(responseMessage);

        return await MakeResponse<T>(responseMessage);
    }

    /// <inheritdoc />
    public Task<T> PostAsync<T>(string url, object data)
    {
        return PostAsync<T>(url, null, data);
    }

    /// <inheritdoc />
    public async Task<T> PostAsync<T>(string url, string bearerToken, object data)
    {
        var responseMessage = await PostAsyncAndGetResponseMessage(url, bearerToken, data);
        return await MakeResponse<T>(responseMessage);
    }

    /// <inheritdoc />
    public async Task<Uri> PostAsyncAndGetLocation(string url, string bearerToken, object data)
    {
        var responseMessage = await PostAsyncAndGetResponseMessage(url, bearerToken, data);
        return responseMessage.Headers.Location;
    }

    /// <inheritdoc />
    public async Task PutAsync(string url, string bearerToken, object? data)
    {
        var client = _httpClientFactory.CreateClient();
        string jsonContent = JsonSerializer.Serialize(data);
        HttpContent content = new StringContent(jsonContent);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json") { CharSet = "UTF-8" };

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri(url),
            Content = content
        };
        request.Headers.Add("Authorization", $"Bearer {bearerToken}");

        var responseMessage = await client.SendAsync(request);
        await EnsureSuccessStatusCode(responseMessage);
    }

    /// <inheritdoc />
    public async Task<T> GetAsync<T>(string url, string bearerToken)
    {
        var client = _httpClientFactory.CreateClient();
        var request = new HttpRequestMessage { Method = HttpMethod.Get, RequestUri = new Uri(url) };
        request.Headers.Add("Authorization", $"Bearer {bearerToken}");

        var responseMessage = await client.SendAsync(request);
        await EnsureSuccessStatusCode(responseMessage);

        return await MakeResponse<T>(responseMessage);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string url, string bearerToken)
    {
        var client = _httpClientFactory.CreateClient();
        var request = new HttpRequestMessage { Method = HttpMethod.Delete, RequestUri = new Uri(url) };
        request.Headers.Add("Authorization", $"Bearer {bearerToken}");

        var responseMessage = await client.SendAsync(request);
        await EnsureSuccessStatusCode(responseMessage);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string url, string bearerToken, object data)
    {
        var client = _httpClientFactory.CreateClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(url),
            Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json"),
        };
        request.Headers.Add("Authorization", $"Bearer {bearerToken}");

        var responseMessage = await client.SendAsync(request);
        await EnsureSuccessStatusCode(responseMessage);
    }

    private async Task<HttpResponseMessage> PostAsyncAndGetResponseMessage(string url, string bearerToken, object data)
    {
        var client = _httpClientFactory.CreateClient();
        string jsonContent = JsonSerializer.Serialize(data);
        HttpContent content = new StringContent(jsonContent);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json") { CharSet = "UTF-8" };

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(url),
            Content = content
        };

        if (!string.IsNullOrWhiteSpace(bearerToken))
        {
            request.Headers.Add("Authorization", $"Bearer {bearerToken}");
        }

        var responseMessage = await client.SendAsync(request);
        await EnsureSuccessStatusCode(responseMessage);

        return responseMessage;
    }

    private static async Task EnsureSuccessStatusCode(HttpResponseMessage responseMessage)
    {
        try
        {
            responseMessage.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            var internalError = await MakeResponse<ErrorResponseJsonData>(responseMessage);
            throw new HttpClientUtilityException(
                ex.Message,
                responseMessage.StatusCode,
                internalError.Error,
                internalError.ErrorDescription
            );
        }
        catch (Exception ex)
        {
            throw new HttpClientUtilityException(ex.Message, responseMessage.StatusCode);
        }
    }

    private static async Task<T> MakeResponse<T>(HttpResponseMessage responseMessage)
    {
        string jsonString = await responseMessage.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(jsonString);
    }
}
