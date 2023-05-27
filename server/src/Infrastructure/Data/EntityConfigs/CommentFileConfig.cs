using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

public class CommentFileConfig : IEntityTypeConfiguration<CommentFile>
{
    public void Configure(EntityTypeBuilder<CommentFile> builder)
    {
        builder.HasKey(cf => cf.Id);

        builder.HasOne(cf => cf.File)
            .WithMany()
            .HasForeignKey(cf => cf.FileId);
        
        builder.HasOne(cf => cf.Comment)
            .WithMany()
            .HasForeignKey(cf => cf.CommentId);
    }
}