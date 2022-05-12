using Quartz;
using RidelyTask.Data.Configuration;
using RidelyTask.Worker;
using RidelyTask.Worker.Abstractions;
using RidelyTask.Worker.Configuration;
using RidelyTask.Worker.Services;

// every 5 seconds
const string cronExpression = "0/5 * * * * ?";

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.AddQuartz(cfg =>
        {
            cfg.UseMicrosoftDependencyInjectionJobFactory();
            
            var jobKey = new JobKey(nameof(VisitorsFilesWorker));
            
            cfg.AddJob<VisitorsFilesWorker>(x => x.WithIdentity(jobKey))
                .AddTrigger(x => x.ForJob(jobKey)
                    .WithIdentity($"{nameof(VisitorsFilesWorker)}-CronTrigger")
                    .WithCronSchedule(cronExpression)
                    .StartNow());
        });

        services.AddQuartzHostedService(cfg => { cfg.WaitForJobsToComplete = true; });

        services.AddDatabase(ctx.Configuration);
        services.AddSingleton<IVisitorsFileStorage, VisitorsFileStorage>();
        
        var fileStorageOptions = new FileStorageOptions();
        ctx.Configuration.GetSection("FileStorage").Bind(fileStorageOptions);;
        services.AddSingleton(fileStorageOptions);
    });

var host = builder.Build();

await host.RunAsync();