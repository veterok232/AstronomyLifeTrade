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
        
        new ProductFile
        {
            Id = Guid.Parse("61cd738b-7252-4a2f-a781-b41548facd56"),
            FileId = Guid.Parse("b8ce5948-a954-455a-9b35-9262732f5253"),
            ProductId = Guid.Parse("9dd4a5ef-bb70-4acd-8c3b-1f5942bedcd0"),
            ProductFileType = ProductFileType.Image
        },
        new ProductFile
        {
            Id = Guid.Parse("01f4f842-ea0b-4512-b6ab-3a31173c1c20"),
            FileId = Guid.Parse("73c424e6-f136-4475-a157-0c298366b964"),
            ProductId = Guid.Parse("9dd4a5ef-bb70-4acd-8c3b-1f5942bedcd0"),
            ProductFileType = ProductFileType.Image
        },
        new ProductFile
        {
            Id = Guid.Parse("094f9482-f6fd-4975-b62a-1fb8d69875a8"),
            FileId = Guid.Parse("3a750d77-80d6-40eb-ad94-4a5cf2d9547c"),
            ProductId = Guid.Parse("9dd4a5ef-bb70-4acd-8c3b-1f5942bedcd0"),
            ProductFileType = ProductFileType.Image
        },
        new ProductFile
        {
            Id = Guid.Parse("b7d4c91f-6c5b-4ef8-981b-8a267c8806f0"),
            FileId = Guid.Parse("35c71e96-3967-411e-9eba-b12119c9bb99"),
            ProductId = Guid.Parse("bb3de5c0-daa1-4b80-8a01-6558005db3c5"),
            ProductFileType = ProductFileType.Image
        },
        new ProductFile
        {
            Id = Guid.Parse("781934a5-8b08-42f2-aebf-af718c7ab199"),
            FileId = Guid.Parse("9135be23-a4b6-488d-8cec-164f5c59cfba"),
            ProductId = Guid.Parse("f6112b01-2549-4a80-98a8-8bb1eaeca160"),
            ProductFileType = ProductFileType.Image
        },
    };
}