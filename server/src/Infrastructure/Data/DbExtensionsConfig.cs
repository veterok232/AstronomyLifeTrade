using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

internal static class DbExtensionsConfig
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("citext");
        modelBuilder.HasPostgresExtension("uuid-ossp");
    }
}