using ApplicationCore.Enums;

namespace ApplicationCore.Entities.InitialData;

public static class FilesInitData
{
    public static readonly File[] Data =
    {
        new File
        {
            Id = Guid.Parse("87b14130-8b4f-46af-a571-a2c5fdd6fefa"),
            FileName = "1",
            Reference = "1671c525-3bee-453f-8a7b-6a2b64ba853c",
            Extension = ".jpg",
            MimeType = "image/jpeg",
            FileSizeInBytes = 250308L,
            AttachmentType = AttachmentType.ProductImage,
            StorageType = FileStorageType.FileSystem,
            CreatedAt = new DateTime(2023, 01, 03, 0, 0, 0, DateTimeKind.Utc),
            IsAttached = true,
        },
        new File
        {
            Id = Guid.Parse("7cba49fe-e562-487a-bfec-4ea235f35ab3"),
            FileName = "2",
            Reference = "9d6dbaf4-60aa-41d8-be34-86357e074146",
            Extension = ".jpg",
            MimeType = "image/jpeg",
            FileSizeInBytes = 256673L,
            AttachmentType = AttachmentType.ProductImage,
            StorageType = FileStorageType.FileSystem,
            CreatedAt = new DateTime(2023, 01, 03, 0, 0, 0, DateTimeKind.Utc),
            IsAttached = true,
        },
        new File
        {
            Id = Guid.Parse("55515111-3b2a-4749-b48b-7e17faa2eaaf"),
            FileName = "3",
            Reference = "588c7a91-a348-4a13-b9f1-0b544d7d7ae8",
            Extension = ".jpg",
            MimeType = "image/jpeg",
            FileSizeInBytes = 230563L,
            AttachmentType = AttachmentType.ProductImage,
            StorageType = FileStorageType.FileSystem,
            CreatedAt = new DateTime(2023, 01, 03, 0, 0, 0, DateTimeKind.Utc),
            IsAttached = true,
        },
        new File
        {
            Id = Guid.Parse("11db01d7-e42f-425c-b312-c3253b5402f3"),
            FileName = "4",
            Reference = "abb6d9a7-9e43-4889-b09e-056ff0480a5b",
            Extension = ".jpg",
            MimeType = "image/jpeg",
            FileSizeInBytes = 495704L,
            AttachmentType = AttachmentType.ProductImage,
            StorageType = FileStorageType.FileSystem,
            CreatedAt = new DateTime(2023, 01, 03, 0, 0, 0, DateTimeKind.Utc),
            IsAttached = true,
        },
        new File
        {
            Id = Guid.Parse("b8ce5948-a954-455a-9b35-9262732f5253"),
            FileName = "1",
            Reference = "66ad078d-aec6-42a1-b181-813749755e5b",
            Extension = ".jpg",
            MimeType = "image/jpeg",
            FileSizeInBytes = 49934L,
            AttachmentType = AttachmentType.ProductImage,
            StorageType = FileStorageType.FileSystem,
            CreatedAt = new DateTime(2023, 01, 05, 0, 0, 0, DateTimeKind.Utc),
            IsAttached = true,
        },
        new File
        {
            Id = Guid.Parse("73c424e6-f136-4475-a157-0c298366b964"),
            FileName = "2",
            Reference = "235598eb-cfee-4f12-a088-e7e707830c4e",
            Extension = ".jpg",
            MimeType = "image/jpeg",
            FileSizeInBytes = 53867L,
            AttachmentType = AttachmentType.ProductImage,
            StorageType = FileStorageType.FileSystem,
            CreatedAt = new DateTime(2023, 01, 05, 0, 0, 0, DateTimeKind.Utc),
            IsAttached = true,
        },
        new File
        {
            Id = Guid.Parse("3a750d77-80d6-40eb-ad94-4a5cf2d9547c"),
            FileName = "3",
            Reference = "3e60e6b0-b478-41be-908e-b884416eb555",
            Extension = ".jpg",
            MimeType = "image/jpeg",
            FileSizeInBytes = 50336L,
            AttachmentType = AttachmentType.ProductImage,
            StorageType = FileStorageType.FileSystem,
            CreatedAt = new DateTime(2023, 01, 05, 0, 0, 0, DateTimeKind.Utc),
            IsAttached = true,
        },
        new File
        {
            Id = Guid.Parse("35c71e96-3967-411e-9eba-b12119c9bb99"),
            FileName = "1",
            Reference = "ed66e85a-722f-48ef-918e-ac552fc36aa7",
            Extension = ".jpg",
            MimeType = "image/jpeg",
            FileSizeInBytes = 39755L,
            AttachmentType = AttachmentType.ProductImage,
            StorageType = FileStorageType.FileSystem,
            CreatedAt = new DateTime(2023, 01, 05, 0, 0, 0, DateTimeKind.Utc),
            IsAttached = true,
        },
        new File
        {
            Id = Guid.Parse("9135be23-a4b6-488d-8cec-164f5c59cfba"),
            FileName = "1",
            Reference = "3de0a642-b806-4664-8165-37efda5719d1",
            Extension = ".jpg",
            MimeType = "image/jpeg",
            FileSizeInBytes = 169124L,
            AttachmentType = AttachmentType.ProductImage,
            StorageType = FileStorageType.FileSystem,
            CreatedAt = new DateTime(2023, 01, 05, 0, 0, 0, DateTimeKind.Utc),
            IsAttached = true,
        }
    };
}