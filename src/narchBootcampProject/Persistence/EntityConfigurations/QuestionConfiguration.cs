using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions").HasKey(q => q.Id);

        builder.Property(q => q.Id).HasColumnName("Id").IsRequired();
        builder.Property(q => q.BootcampId).HasColumnName("BootcampId");
        builder.Property(q => q.Text).HasColumnName("Text");
        builder.Property(q => q.AnswerA).HasColumnName("AnswerA");
        builder.Property(q => q.AnswerB).HasColumnName("AnswerB");
        builder.Property(q => q.AnswerC).HasColumnName("AnswerC");
        builder.Property(q => q.AnswerD).HasColumnName("AnswerD");
        builder.Property(q => q.CorrectAnswer).HasColumnName("CorrectAnswer");
        builder.Property(q => q.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(q => q.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(q => q.DeletedDate).HasColumnName("DeletedDate");

        builder.HasMany(x => x.QuizQuestions);
        builder.HasOne(x => x.Bootcamp);

        builder.HasQueryFilter(q => !q.DeletedDate.HasValue);
    }
}
