
namespace Shared.Application.Abstractions;

public interface IFileStorage
{
    Task<string> SaveAsync(string fileName, Stream content);
    Task DeleteAsync(string filePath);
    Task<Stream> GetAsync(string filePath);
}