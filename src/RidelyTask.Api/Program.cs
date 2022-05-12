using MediatR;
using RidelyTask.Api.Abstractions;
using RidelyTask.Api.Configuration;
using RidelyTask.Api.Services;
using RidelyTask.Data.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddSingleton<IVisitorsFileStorage, VisitorsesFileStorage>();

var fileStorageOptions = new FileStorageOptions();
builder.Configuration.GetSection("FileStorage").Bind(fileStorageOptions);
builder.Services.AddSingleton(fileStorageOptions);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.InitializeDatabase();

app.Run();