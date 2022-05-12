namespace RidelyTask.Api.Abstractions;

public interface IVisitorsFileStorage
{
    Task StoreAsync(string id, Stream fileContent, CancellationToken cancellationToken = default);
}