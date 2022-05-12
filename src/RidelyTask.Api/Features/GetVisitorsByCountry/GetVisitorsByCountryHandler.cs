using MediatR;
using Microsoft.EntityFrameworkCore;
using RidelyTask.Data.Context;

namespace RidelyTask.Api.Features.GetVisitorsByCountry;

public class GetVisitorsByCountryHandler
    : IRequestHandler<GetVisitorsByCountryRequestDto, GetVisitorsByCountryResultDto>
{
    private readonly RidelyDbContext _dbContext;

    public GetVisitorsByCountryHandler(RidelyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetVisitorsByCountryResultDto> Handle(GetVisitorsByCountryRequestDto request,
        CancellationToken cancellationToken)
        => new()
        {
            CountryVisitors = await _dbContext.VisitorsRecords.GroupBy(x => x.Country)
                .Select(x => new CountryVisitors
                {
                    Country = x.Key,
                    Visitors = x.Sum(y => y.Visitors)
                })
                .ToListAsync(cancellationToken)
        };
}