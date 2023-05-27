using ApplicationCore.Entities;
using ApplicationCore.Entities.InitialData;
using Microsoft.EntityFrameworkCore;
using File = ApplicationCore.Entities.File;

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
        modelBuilder.Entity<Telescope>().HasData(TelescopesInitData.Data);
        modelBuilder.Entity<File>().HasData(FilesInitData.Data);
        modelBuilder.Entity<ProductFile>().HasData(ProductFilesInitData.Data);
    }
}