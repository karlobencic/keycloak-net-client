namespace NextLevelDev.Keycloak.Utility.Extensions;

/// <summary>
/// Defines extensions for collection types
/// </summary>
internal static class CollectionExtensions
{
    /// <summary>
    /// Convert enumerable to readonly collection
    /// </summary>
    /// <typeparam name="T">Enumerable's type</typeparam>
    /// <param name="items">Enumerable</param>
    /// <returns>Readonly collection</returns>
    internal static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T>? items)
    {
        items ??= Enumerable.Empty<T>();

        return items.ToList().AsReadOnly();
    }
}
