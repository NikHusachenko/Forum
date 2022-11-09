using Forum.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.EntityFramework.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<AnswerEntity>
    {
        public void Configure(EntityTypeBuilder<AnswerEntity> builder)
        {
            builder.ToTable("Answers").HasKey(answer => answer.Id);

            builder.Property(answer => answer.Body).HasMaxLength(1023);

            builder.HasMany<AnswerEntity>(answer => answer.Answers)
                .WithOne(answer => answer.PreviousAnswer)
                .HasForeignKey(answer => answer.ProviousFK);
        }
    }
}