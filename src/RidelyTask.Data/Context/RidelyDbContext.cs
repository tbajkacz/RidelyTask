using Microsoft.EntityFrameworkCore;
using RidelyTask.Data.Models;

namespace RidelyTask.Data.Context;

public class RidelyDbContext : DbContext
{
    public RidelyDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<VisitorsRecord> VisitorsRecords { get; set; }
}