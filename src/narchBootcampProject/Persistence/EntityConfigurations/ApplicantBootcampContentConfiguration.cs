using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ApplicantBootcampContentConfiguration : IEntityTypeConfiguration<ApplicantBootcampContent>
{
    public void Configure(EntityTypeBuilder<ApplicantBootcampContent> builder)
    {
        builder.ToTable("ApplicantBootcampContents").HasKey(abc => abc.Id);

        builder.Property(abc => abc.Id).HasColumnName("Id").IsRequired();
        builder.Property(abc => abc.ApplicantId).HasColumnName("ApplicantId");
        builder.Property(abc => abc.BootcampContentId).HasColumnName("BootcampContentId");
        builder.Property(abc => abc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(abc => abc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(abc => abc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(x => x.BootcampContent);
        builder.HasOne(x => x.Applicant);
        builder.HasQueryFilter(abc => !abc.DeletedDate.HasValue);
    }
}