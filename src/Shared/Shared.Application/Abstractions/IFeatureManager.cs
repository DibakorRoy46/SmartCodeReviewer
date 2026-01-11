
namespace Shared.Application.Abstractions;

public interface IFeatureManager
{
    Task<bool> IsEnabledAsync(string featureName);
}
