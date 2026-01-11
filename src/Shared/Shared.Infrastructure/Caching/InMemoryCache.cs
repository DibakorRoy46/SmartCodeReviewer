
using Shared.Application.Abstractions;
using System.Collections.Concurrent;

namespace Shared.Infrastructure.Caching;

public class InMemoryCache : ICache
{
    private readonly ConcurrentDictionary<string, object> _cache = new();

    public Task SetAsync<T>(string key, T value, TimeSpan? ttl = null)
    {
        _cache[key] = value!;
        return Task.CompletedTask;
    }

    public Task<T?> GetAsync<T>(string key)
    {
        _cache.TryGetValue(key, out var value);
        return Task.FromResult((T?)value);
    }

    public Task RemoveAsync(string key)
    {
        _cache.TryRemove(key, out _);
        return Task.CompletedTask;
    }
}