namespace LearnLanguageSystem.Services.Data.Tests.Answers
{
    using System.Linq;
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.Answers;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class AnswersServiceTests : BaseServiceTests
    {
        private const string AnswerId = "429f9696-1d36-423d-a9ab-f1990018b017";

        public AnswersServiceTests()
        {
            this.Answer = new Answer
            {
                Id = AnswerId,
                IsRight = true,
                Content = "TestContent",
            };

            this.DbContext.Answers.Add(this.Answer);
            this.DbContext.SaveChanges();
        }

        private Answer Answer { get; set; }

        private IAnswersService Service => this.ServiceProvider.GetRequiredService<IAnswersService>();

        [Fact]
        public async Task CreateAsyncShouldReturnTrueAndCreateAnswer()
        {
            var id = "1234";
            var content = "NewContent";
            var isRight = false;

            var result = await this.Service.CreateAsync(id, content, isRight);

            Assert.True(result);
            Assert.Equal(2, this.DbContext.Answers.Count());
        }
    }
}
