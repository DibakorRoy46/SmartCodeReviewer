
using Shared.Application.Abstractions;

namespace Shared.Infrastructure.FeatureFlags;

public class FeatureManager : IFeatureManager
{
    private readonly Dictionary<string, bool> _features = new();

    public FeatureManager(Dictionary<string, bool>? features = null)
    {
        _features = features ?? new Dictionary<string, bool>();
    }

    public Task<bool> IsEnabledAsync(string featureName)
    {
        _features.TryGetValue(featureName, out var enabled);
        return Task.FromResult(enabled);
    }
}