using Infrastructure.Data.EntityConfigs;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DbExtensionsConfig.Configure(modelBuilder);
        DbSequenceConfig.Configure(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.ConfigureOptimisticLock();

        InitialDataConfigurator.SetupSystemData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}