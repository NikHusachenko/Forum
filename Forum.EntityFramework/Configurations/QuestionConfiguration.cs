using Forum.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.EntityFramework.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<QuestionEntity>
    {
        public void Configure(EntityTypeBuilder<QuestionEntity> builder)
        {
            builder.ToTable("Questions").HasKey(question => question.Id);

            builder.Property(question => question.Title).HasMaxLength(127);
            builder.Property(question => question.Body).HasMaxLength(1023);

            builder.HasMany<AnswerEntity>(question => question.Answers)
                .WithOne(answer => answer.Question)
                .HasForeignKey(answer => answer.QuestionFK);
        }
    }
}