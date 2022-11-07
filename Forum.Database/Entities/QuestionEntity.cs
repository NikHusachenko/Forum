namespace Forum.Database.Entities
{
    public class QuestionEntity
    {
        public QuestionEntity()
        {
            Answers = new List<AnswerEntity>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        public long? UserFK { get; set; }
        public UserEntity UserEntity { get; set; }

        public ICollection<AnswerEntity> Answers { get; set; }
    }
}