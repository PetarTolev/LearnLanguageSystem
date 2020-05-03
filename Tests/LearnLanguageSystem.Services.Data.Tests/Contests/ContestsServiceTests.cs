namespace LearnLanguageSystem.Services.Data.Tests.Contests
{
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.Contests;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class ContestsServiceTests : BaseServiceTests
    {
        private const string ContestId = "33f3cf66-1f36-4cb7-ae58-d91f3984af88";
        private const string QuestionId = "4014ce33-3081-458d-9ae1-0251f9abf1f2";
        private const string AnswerId = "219c1100-e839-4c66-afc3-89c96cfa37a5";
        private const string CreatorId = "34c9e30d-b074-4f75-9f6e-f2da2377a6bf";

        public ContestsServiceTests()
        {
            var answer = new Answer
            {
                Id = AnswerId,
                IsRight = true,
                Content = "TestAnswerContest",
            };

            var question = new Question
            {
                Id = QuestionId,
                Content = "TestQuestionContent",
            };

            question.Answers.Add(answer);

            var creator = new ApplicationUser
            {
                Id = CreatorId,
                UserName = "CreatorUser",
                AvatarUrl = "TestAvatarUrl",
            };

            this.Contest = new Contest
            {
                Id = ContestId,
                Creator = creator,
                Name = "TestContestName",
            };

            this.Contest.Questions.Add(question);

            this.DbContext.Add(this.Contest);
            this.DbContext.SaveChanges();
        }

        private IContestsService Service => this.ServiceProvider.GetRequiredService<IContestsService>();

        private Contest Contest { get; set; }

        [Fact]
        public void GetByIdShouldReturnCorrectType()
        {
        }

        [Fact]
        public void GetByIdShouldReturnCorrectContest()
        {
        }

        [Fact]
        public void GetByIdShouldReturnNullWhenContestNotExist()
        {
        }

        [Fact]
        public void GetQuestionShouldReturnCorrectType()
        {
        }

        [Fact]
        public void GetQuestionShouldReturnCorrectCount()
        {
        }

        [Fact]
        public void GetQuestionShouldReturnCorrectQuestions()
        {
        }

        [Fact]
        public void GetCreatorIdShouldReturnCorrectId()
        {
        }

        [Fact]
        public void GetCreatorIdShouldReturnNullWhenCreatorNotExist()
        {
        }

        [Fact]
        public void GetOwnedShouldReturnCorrectCount()
        {
        }

        [Fact]
        public void GetOwnedShouldReturnCorrectType()
        {
        }

        [Fact]
        public void GetOwnedShouldReturnCorrectNullWhenOwnerNotExist()
        {
        }
    }
}
