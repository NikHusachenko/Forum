namespace Forum.Database.Entities
{
    public class UserEntity
    {
        public UserEntity()
        {
            Questions = new List<QuestionEntity>();
            Answers = new List<AnswerEntity>();
        }

        public long Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        public ICollection<QuestionEntity> Questions { get; set; }
        public ICollection<AnswerEntity> Answers { get; set; }
    }
}