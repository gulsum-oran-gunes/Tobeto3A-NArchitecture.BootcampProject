using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.ToTable("Quizs").HasKey(q => q.Id);

        builder.Property(q => q.Id).HasColumnName("Id").IsRequired();
        builder.Property(q => q.ApplicantId).HasColumnName("ApplicantId");
        builder.Property(q => q.BootcampId).HasColumnName("BootcampId");
        builder.Property(q => q.StartTime).HasColumnName("StartTime").IsRequired(false);
        builder.Property(q => q.EndTime).HasColumnName("EndTime").IsRequired(false);

        builder.Property(q => q.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(q => q.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(q => q.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(x => x.Applicant);
        builder.HasOne(x => x.Bootcamp);
        builder.HasMany(x => x.QuizQuestions);

        builder.HasQueryFilter(q => !q.DeletedDate.HasValue);
    }
}
