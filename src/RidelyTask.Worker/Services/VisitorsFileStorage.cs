using RidelyTask.Worker.Abstractions;
using RidelyTask.Worker.Configuration;

namespace RidelyTask.Worker.Services;

public class VisitorsFileStorage : IVisitorsFileStorage
{
    private readonly FileStorageOptions _fileStorageOptions;

    public VisitorsFileStorage(FileStorageOptions fileStorageOptions)
    {
        _fileStorageOptions = fileStorageOptions;
    }
    
    public Task<IEnumerable<string>> GetFileListAsync(CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(_fileStorageOptions.VisitorsStorageDirectory);
        
        return Task.FromResult(Directory.GetFiles(_fileStorageOptions.VisitorsStorageDirectory).AsEnumerable());
    }

    public Task<string> GetContentAsync(string fileName, CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(_fileStorageOptions.VisitorsStorageDirectory);
        
        return File.ReadAllTextAsync(fileName, cancellationToken);
    }

    public Task DeleteAsync(string fileName, CancellationToken cancellationToken = default)
    {
        File.Delete(fileName);

        return Task.CompletedTask;
    }
}