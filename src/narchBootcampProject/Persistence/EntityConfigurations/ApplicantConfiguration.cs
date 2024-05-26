using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.ToTable("Applicants");

        builder.Property(a => a.Id).HasColumnName("Id").IsRequired();
        builder.Property(a => a.About).HasColumnName("About").IsRequired(false);
        builder.Property(a => a.EmailVerified).HasColumnName("EmailVerified").IsRequired(false);
        builder.Property(a => a.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(a => a.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(a => a.DeletedDate).HasColumnName("DeletedDate");
        builder.HasMany(x => x.Quizzes);

        builder.HasMany(x => x.ApplicationEntities);
        builder.HasMany(x => x.ApplicantBootcampContents);
        builder.HasMany(x => x.Certificates);
        //builder.HasQueryFilter(a => !a.DeletedDate.HasValue);
    }
}
