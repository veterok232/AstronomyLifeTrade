using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

internal class FileConfig : IEntityTypeConfiguration<ApplicationCore.Entities.File>
{
    public void Configure(EntityTypeBuilder<ApplicationCore.Entities.File> builder)
    {
        builder.HasKey(key => key.Id);

        builder.Property(p => p.FileName).HasMaxLength(DbConfigConstants.DefaultTextColumnLength).IsRequired();
        builder.Property(p => p.Reference).HasMaxLength(DbConfigConstants.DefaultTextColumnLength).IsRequired();
        builder.Property(p => p.MimeType).HasMaxLength(DbConfigConstants.DefaultTextColumnLength).IsRequired();
        builder.Property(p => p.Extension).HasMaxLength(DbConfigConstants.DefaultTextColumnLength).IsRequired();
        builder.Property(p => p.FileSizeInBytes).IsRequired();
        builder.Property(p => p.CreatedAt).IsRequired();
        
        builder.HasOne(a => a.Assignment)
            .WithMany(a => a.AvatarFiles)
            .HasForeignKey(f => f.AssignmentId);
    }
}