namespace LearnLanguageSystem.Data.Seeding.Dtos
{
    public class QuestionDto
    {
        public string Content { get; set; }

        public AnswerDto[] Answers { get; set; }
    }
}
