using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Quartz;
using RidelyTask.Data.Context;
using RidelyTask.Data.Models;
using RidelyTask.Worker.Abstractions;

namespace RidelyTask.Worker;

[DisallowConcurrentExecution]
public class VisitorsFilesWorker : IJob
{
    private readonly ILogger<VisitorsFilesWorker> _logger;
    private readonly IVisitorsFileStorage _visitorsFileStorage;
    private readonly RidelyDbContext _dbContext;

    public VisitorsFilesWorker(ILogger<VisitorsFilesWorker> logger, IVisitorsFileStorage visitorsFileStorage, RidelyDbContext dbContext)
    {
        _logger = logger;
        _visitorsFileStorage = visitorsFileStorage;
        _dbContext = dbContext;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation($"Executing {nameof(Execute)} in {nameof(VisitorsFilesWorker)}");
        
        try
        {
            await ExecuteAsync(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        
        _logger.LogInformation($"Finished executing {nameof(Execute)} in {nameof(VisitorsFilesWorker)}");
    }

    private async Task ExecuteAsync(IJobExecutionContext context)
    {
        var fileNames = await _visitorsFileStorage.GetFileListAsync();

        foreach (var fileName in fileNames)
        {
            var fileContent = await _visitorsFileStorage.GetContentAsync(fileName);

            var visitorsRecord = JsonConvert.DeserializeObject<VisitorsRecord>(fileContent);
            visitorsRecord.Id = fileName;
            visitorsRecord.Date = visitorsRecord.Date.ToUniversalTime();

            await _dbContext.VisitorsRecords.AddAsync(visitorsRecord, context.CancellationToken);

            await _dbContext.SaveChangesAsync(context.CancellationToken);

            await _visitorsFileStorage.DeleteAsync(fileName);
        }
    }
}