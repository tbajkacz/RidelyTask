namespace RidelyTask.Api.Features.GetVisitorsByCountry;

public class GetVisitorsByCountryResultDto
{
    public IEnumerable<CountryVisitors> CountryVisitors { get; set; }
}

public class CountryVisitors
{
    public long Visitors { get; set; }

    public string Country { get; set; }
}