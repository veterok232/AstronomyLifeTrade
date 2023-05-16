using ApplicationCore.Entities;
using ApplicationCore.Entities.InitialData;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

internal static class InitialDataConfigurator
{
    public static void SetupSystemData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(RolesInitData.Data);
        modelBuilder.Entity<User>().HasData(UsersInitData.Data);
        modelBuilder.Entity<Assignment>().HasData(AssignmentsInitData.Data);
        modelBuilder.Entity<Brand>().HasData(BrandsInitData.Data);
        modelBuilder.Entity<Category>().HasData(CategoryInitData.Data);
        modelBuilder.Entity<Product>().HasData(ProductsInitData.Data);
    }
}