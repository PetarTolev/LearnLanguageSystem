namespace LearnLanguageSystem.Services.Data.Tests.Questions
{
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.Questions;
    using Microsoft.Extensions.DependencyInjection;

    public class QuestionsServiceTests : BaseServiceTests
    {
        private const string QuestionId = "";
        private const string AnswerId = "";

        public QuestionsServiceTests()
        {
            var answer = new Answer
            {
                Id = AnswerId,
                Content = "TestAnswerContent",
                IsRight = true,
            };

            this.Question = new Question
            {
                Id = QuestionId,
                Content = "TestQuestionContent",
            };

            this.Question.Answers.Add(answer);
            this.DbContext.Add(this.Question);
            this.DbContext.SaveChanges();
        }

        private Question Question { get; set; }

        private IQuestionsService questionsService => this.ServiceProvider.GetRequiredService<IQuestionsService>();
    }
}
