
namespace Shared.Application.Abstractions;

public interface ICache
{
    Task SetAsync<T>(string key, T value, TimeSpan? ttl = null);
    Task<T?> GetAsync<T>(string key);
    Task RemoveAsync(string key);
}
