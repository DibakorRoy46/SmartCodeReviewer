
using Shared.Application.Abstractions;

namespace Shared.Infrastructure.FileStorage;

public class LocalFileStorage : IFileStorage
{
    private readonly string _basePath;

    public LocalFileStorage(string basePath)
    {
        _basePath = basePath;
        Directory.CreateDirectory(_basePath);
    }

    public async Task<string> SaveAsync(string fileName, Stream content)
    {
        var filePath = Path.Combine(_basePath, $"{Guid.NewGuid()}_{fileName}");
        using var fs = new FileStream(filePath, FileMode.Create);
        await content.CopyToAsync(fs);
        return filePath;
    }

    public Task DeleteAsync(string filePath)
    {
        if (File.Exists(filePath))
            File.Delete(filePath);

        return Task.CompletedTask;
    }

    public Task<Stream> GetAsync(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException(filePath);

        Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return Task.FromResult(stream);
    }
}

