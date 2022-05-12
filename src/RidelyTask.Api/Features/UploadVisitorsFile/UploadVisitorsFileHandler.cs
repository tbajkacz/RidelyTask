using MediatR;
using RidelyTask.Api.Abstractions;

namespace RidelyTask.Api.Features.UploadVisitorsFile;

public class UploadVisitorsFileHandler : IRequestHandler<UploadVisitorsFileRequestDto>
{
    private readonly IVisitorsFileStorage _visitorsFileStorage;

    public UploadVisitorsFileHandler(IVisitorsFileStorage visitorsFileStorage)
    {
        _visitorsFileStorage = visitorsFileStorage;
    }
    
    public async Task<Unit> Handle(UploadVisitorsFileRequestDto request, CancellationToken cancellationToken)
    {
        if (request.FileExtension is not ".json")
        {
            throw new("Only files with a .json extension are accepted");
        }

        await _visitorsFileStorage.StoreAsync(Guid.NewGuid().ToString(), request.FileContent, cancellationToken);
        
        return Unit.Value;
    }
}