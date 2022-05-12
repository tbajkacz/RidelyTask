namespace RidelyTask.Worker.Abstractions;

public interface IVisitorsFileStorage
{
    Task<IEnumerable<string>> GetFileListAsync(CancellationToken cancellationToken = default);

    Task<string> GetContentAsync(string fileName, CancellationToken cancellationToken = default);

    Task DeleteAsync(string fileName, CancellationToken cancellationToken = default);
}