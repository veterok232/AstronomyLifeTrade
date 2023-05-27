using ApplicationCore.Enums;

namespace ApplicationCore.Entities.InitialData;

public static class ProductFilesInitData
{
    public static readonly ProductFile[] Data =
    {
        new ProductFile
        {
            Id = Guid.Parse("f398e48d-3c6c-4c9b-90f1-463263d3091a"),
            FileId = Guid.Parse("87b14130-8b4f-46af-a571-a2c5fdd6fefa"),
            ProductId = Guid.Parse("26e52bab-4c7e-4f92-b46e-8708b1e4f302"),
            ProductFileType = ProductFileType.Image
        },
        new ProductFile
        {
            Id = Guid.Parse("8e25b766-1361-43ea-8084-2428d0699975"),
            FileId = Guid.Parse("7cba49fe-e562-487a-bfec-4ea235f35ab3"),
            ProductId = Guid.Parse("26e52bab-4c7e-4f92-b46e-8708b1e4f302"),
            ProductFileType = ProductFileType.Image
        },
        new ProductFile
        {
            Id = Guid.Parse("639a3b65-3376-4927-9c37-8d76e9d715c2"),
            FileId = Guid.Parse("55515111-3b2a-4749-b48b-7e17faa2eaaf"),
            ProductId = Guid.Parse("26e52bab-4c7e-4f92-b46e-8708b1e4f302"),
            ProductFileType = ProductFileType.Image
        },
        new ProductFile
        {
            Id = Guid.Parse("eab8c7de-e063-4720-a15b-623cdfc25652"),
            FileId = Guid.Parse("11db01d7-e42f-425c-b312-c3253b5402f3"),
            ProductId = Guid.Parse("26e52bab-4c7e-4f92-b46e-8708b1e4f302"),
            ProductFileType = ProductFileType.Image
        },
    };
}