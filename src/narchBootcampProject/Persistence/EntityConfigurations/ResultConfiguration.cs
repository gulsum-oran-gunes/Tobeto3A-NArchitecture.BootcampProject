using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ResultConfiguration : IEntityTypeConfiguration<Result>
{
    public void Configure(EntityTypeBuilder<Result> builder)
    {
        builder.ToTable("Results").HasKey(r => r.Id);

        builder.Property(r => r.Id).HasColumnName("Id").IsRequired();
        builder.Property(r => r.QuizId).HasColumnName("QuizId");
        builder.Property(r => r.CorrectAnswers).HasColumnName("CorrectAnswers");
        builder.Property(r => r.WrongAnswers).HasColumnName("WrongAnswers");
        builder.Property(r => r.IsPassed).HasColumnName("IsPassed").IsRequired(false);

        builder.Property(r => r.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(r => r.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(r => r.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(x => x.Quiz);
        builder.HasQueryFilter(r => !r.DeletedDate.HasValue);
    }
}
