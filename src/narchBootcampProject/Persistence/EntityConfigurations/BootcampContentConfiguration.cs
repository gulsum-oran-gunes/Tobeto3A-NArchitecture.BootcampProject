using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BootcampContentConfiguration : IEntityTypeConfiguration<BootcampContent>
{
    public void Configure(EntityTypeBuilder<BootcampContent> builder)
    {
        builder.ToTable("BootcampContents").HasKey(bc => bc.Id);

        builder.Property(bc => bc.Id).HasColumnName("Id").IsRequired();
        builder.Property(bc => bc.BootcampId).HasColumnName("BootcampId");
        builder.Property(bc => bc.VideoUrl).HasColumnName("VideoUrl").IsRequired(false);
        builder.Property(bc => bc.Content).HasColumnName("Content").IsRequired(false);
        builder.Property(bc => bc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(bc => bc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(bc => bc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(x => x.Bootcamp);
        builder.HasMany(x => x.ApplicantBootcampContents);
        builder.HasQueryFilter(bc => !bc.DeletedDate.HasValue);
       
    }
}