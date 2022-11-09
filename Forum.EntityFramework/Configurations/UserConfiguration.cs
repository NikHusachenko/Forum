using Forum.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.EntityFramework.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users").HasKey(user => user.Id);

            builder.Property(user => user.Email).HasMaxLength(127);
            builder.Property(user => user.Login).HasMaxLength(63);
            builder.Property(user => user.Password).HasMaxLength(63);

            builder.HasMany<QuestionEntity>(user => user.Questions)
                .WithOne(question => question.UserEntity)
                .HasForeignKey(question => question.UserFK);

            builder.HasMany<AnswerEntity>(user => user.Answers)
                .WithOne(answer => answer.UserEntity)
                .HasForeignKey(answer => answer.UserFK);

            builder.HasData(new List<UserEntity>()
            {
                new UserEntity()
                {
                    Id = 1,
                    CreatedOn = DateTime.Now,
                    Email = "nikgusachenko@gmail.com",
                    Login = "Faraday",
                    Password = "1111",
                }
            });
        }
    }
}