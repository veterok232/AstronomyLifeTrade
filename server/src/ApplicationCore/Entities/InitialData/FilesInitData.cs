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
        }
    };
}