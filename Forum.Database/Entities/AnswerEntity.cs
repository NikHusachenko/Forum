namespace Forum.Database.Entities
{
    public class AnswerEntity
    {
        public AnswerEntity()
        {
            Answers = new List<AnswerEntity>();
        }

        public long Id { get; set; }
        public string Body { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        public long QuestionFK { get; set; }
        public QuestionEntity Question { get; set; }

        public long? UserFK { get; set; }
        public UserEntity UserEntity { get; set; }

        public long? ProviousFK { get; set; }
        public AnswerEntity PreviousAnswer { get; set; }

        public ICollection<AnswerEntity> Answers { get; set; }
    }
}