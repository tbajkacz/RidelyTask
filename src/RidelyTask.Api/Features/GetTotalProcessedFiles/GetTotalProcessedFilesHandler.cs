using MediatR;
using Microsoft.EntityFrameworkCore;
using RidelyTask.Data.Context;

namespace RidelyTask.Api.Features.GetTotalProcessedFiles;

public class GetTotalProcessedFilesHandler
    : IRequestHandler<GetTotalProcessedFilesRequestDto, GetTotalProcessedFilesResultDto>
{
    private readonly RidelyDbContext _dbContext;

    public GetTotalProcessedFilesHandler(RidelyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetTotalProcessedFilesResultDto> Handle(GetTotalProcessedFilesRequestDto request,
        CancellationToken cancellationToken)
        => new()
        {
            ProcessedFilesCount = await _dbContext.VisitorsRecords.CountAsync(cancellationToken)
        };
}