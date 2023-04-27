using ApplicationCore.Entities;
using ApplicationCore.Entities.InitialData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;

internal static class InitialDataConfigurator
{
    public static void SetupSystemData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(RolesInitData.Data);
        modelBuilder.Entity<User>().HasData(UsersInitData.Data);
        modelBuilder.Entity<Assignment>().HasData(AssignmentsInitData.Data);
    }
}