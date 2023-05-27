using ApplicationCore.Enums;
using File = ApplicationCore.Entities.File;

namespace ApplicationCore.Services.Files;

internal class FileBuilder
{
    private readonly Entities.File _file;
    
    private bool _withoutOwner;

    public FileBuilder()
        : this(CreateDefaultFile())
    {
    }

    public FileBuilder(Entities.File file)
    {
        _file = file;
        _withoutOwner = false;

        if (string.IsNullOrWhiteSpace(_file.Reference))
        {
            GenerateReference(_file);
        }
    }

    private static Entities.File CreateDefaultFile()
    {
        return new Entities.File
        {
            IsAttached = true,
        };
    }

    public Entities.File Build()
    {
        return _file;
    }

    public FileBuilder WithoutOwner()
    {
        _withoutOwner = true;

        return this;
    }

    public FileBuilder WithOwnerAssignment(Guid ownerAssignmentId)
    {
        _file.AssignmentId = ownerAssignmentId;

        return this;
    }

    public FileBuilder WithName(string name)
    {
        _file.FileName = name;

        return this;
    }

    public FileBuilder WithMimeType(string mimeType)
    {
        _file.MimeType = mimeType;

        return this;
    }

    public FileBuilder WithExtension(string extension)
    {
        _file.Extension = extension;

        return this;
    }

    public FileBuilder WithSize(long sizeInBytes)
    {
        _file.FileSizeInBytes = sizeInBytes;

        return this;
    }

    public FileBuilder WithAttachmentType(AttachmentType attachmentType)
    {
        _file.AttachmentType = attachmentType;

        return this;
    }

    public FileBuilder WithStorageType(FileStorageType storageType)
    {
        _file.StorageType = storageType;

        return this;
    }

    public FileBuilder Unattached()
    {
        _file.IsAttached = false;

        return this;
    }

    public FileBuilder IsAttached(bool isAttached)
    {
        _file.IsAttached = isAttached;

        return this;
    }

    private static void GenerateReference(File file)
    {
        file.Reference = Guid.NewGuid().ToString();
    }
}