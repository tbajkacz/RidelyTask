using RidelyTask.Api.Abstractions;
using RidelyTask.Api.Configuration;

namespace RidelyTask.Api.Services;

public class VisitorsesFileStorage : IVisitorsFileStorage
{
    private readonly FileStorageOptions _fileStorageOptions;

    public VisitorsesFileStorage(FileStorageOptions fileStorageOptions)
    {
        _fileStorageOptions = fileStorageOptions;
    }
    
    public async Task StoreAsync(string id, Stream fileContent, CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(_fileStorageOptions.VisitorsStorageDirectory);
        
        await using var fileStream =
            new FileStream(Path.Combine(_fileStorageOptions.VisitorsStorageDirectory, id), FileMode.Create);

        await fileContent.CopyToAsync(fileStream, cancellationToken);
    }
}