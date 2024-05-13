using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class InstructorImageConfiguration : IEntityTypeConfiguration<InstructorImage>
{
    public void Configure(EntityTypeBuilder<InstructorImage> builder)
    {
        builder.ToTable("InstructorImages").HasKey(ii => ii.Id);

        builder.Property(ii => ii.Id).HasColumnName("Id").IsRequired();
        builder.Property(ii => ii.InstructorId).HasColumnName("InstructorId");
        builder.Property(ii => ii.ImagePath).HasColumnName("ImagePath");
        builder.Property(ii => ii.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ii => ii.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ii => ii.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(x => x.Instructor);

        builder.HasQueryFilter(ii => !ii.DeletedDate.HasValue);
    }
}