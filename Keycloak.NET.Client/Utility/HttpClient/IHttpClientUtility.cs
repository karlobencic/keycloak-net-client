namespace NextLevelDev.Keycloak.Utility.HttpClient;

/// <summary>
/// Defines methods for communication with web api
/// </summary>
public interface IHttpClientUtility
{
    /// <summary>
    /// Sends query string data to defined location as HTTP post method
    /// </summary>
    /// <typeparam name="T">type of data being returned in response</typeparam>
    /// <param name="url">location and data</param>
    /// <returns>response of type T</returns>
    Task<T> PostAsync<T>(string url);

    /// <summary>
    /// Sends name-value collection data to defined location as HTTP post method using basic authentication.
    /// Uses application/x-www-form-urlencoded format
    /// </summary>
    /// <typeparam name="T">type of data being returned in response</typeparam>
    /// <param name="url">location</param>
    /// <param name="username">username for basic authentication</param>
    /// <param name="password">password for basic authentication</param>
    /// <param name="nameValueCollection">name-value collection data</param>
    /// <returns>response of type T</returns>
    Task<T> PostAsFormDataAsync<T>(
        string url,
        string username,
        string password,
        IReadOnlyCollection<KeyValuePair<string, string>> nameValueCollection
    );

    /// <summary>
    /// Sends name-value collection data to defined location as HTTP post method using bearer authentication
    /// Uses application/x-www-form-urlencoded format
    /// </summary>
    /// <typeparam name="T">type of data being returned in response</typeparam>
    /// <param name="url">send location</param>
    /// <param name="bearerToken">bearer token</param>
    /// <param name="nameValueCollection">name-value collection data</param>
    /// <returns>response of type T</returns>
    Task<T> PostAsFormDataAsync<T>(string url, string? bearerToken, IReadOnlyCollection<KeyValuePair<string, string>> nameValueCollection);

    /// <summary>
    /// Sends json data to defined location as HTTP post method. Json data is contained in http message body.
    /// </summary>
    /// <typeparam name="T">type of data being returned in response</typeparam>
    /// <param name="url">location</param>
    /// <param name="data">json data</param>
    /// <returns>response of type T</returns>
    Task<T> PostAsync<T>(string url, object data);

    /// <summary>
    /// Sends json data to defined location as HTTP post method using bearer authentication. Json data is contained in http message body.
    /// </summary>
    /// <typeparam name="T">type of data being returned in response</typeparam>
    /// <param name="url">location</param>
    /// <param name="bearerToken">bearer token</param>
    /// <param name="data">json data</param>
    /// <returns>response of type T</returns>
    Task<T> PostAsync<T>(string url, string bearerToken, object data);

    /// <summary>
    /// Sends json data to defined location as HTTP post method using bearer authentication. Json data is contained in http message body.
    /// </summary>
    /// <param name="url">location</param>
    /// <param name="bearerToken">bearer token</param>
    /// <param name="data">json data</param>
    /// <returns>location of posted data</returns>
    Task<Uri> PostAsyncAndGetLocation(string url, string bearerToken, object data);

    /// <summary>
    /// Sends json data to defined location as HTTP put method using bearer authentication. Json data is contained in http message body.
    /// </summary>
    /// <param name="url">location</param>
    /// <param name="bearerToken">bearer token</param>
    /// <param name="data">json data</param>
    /// <returns></returns>
    Task PutAsync(string url, string bearerToken, object? data);

    /// <summary>
    /// Receives data of type T through HTTP get method using bearer authentication.
    /// </summary>
    /// <typeparam name="T">type of received data</typeparam>
    /// <param name="url">location</param>
    /// <param name="bearerToken">bearer token</param>
    /// <returns>Received data. Serialized from json data.</returns>
    Task<T> GetAsync<T>(string url, string bearerToken);

    /// <summary>
    /// Deletes data from defined location using bearer authentication
    /// </summary>
    /// <param name="url">location</param>
    /// <param name="bearerToken"></param>
    /// <returns></returns>
    Task DeleteAsync(string url, string bearerToken);

    /// <summary>
    /// Deletes data from defined location using bearer authentication. Data to delete included in body.
    /// </summary>
    /// <param name="url">location</param>
    /// <param name="bearerToken"></param>
    /// <param name="data">data to delete</param>
    /// <returns></returns>
    Task DeleteAsync(string url, string bearerToken, object data);
}
