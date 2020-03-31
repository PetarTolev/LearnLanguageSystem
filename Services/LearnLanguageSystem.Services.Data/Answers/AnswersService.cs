namespace LearnLanguageSystem.Services.Data.Answers
{
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Common.Repositories;
    using LearnLanguageSystem.Data.Models.Contest;

    public class AnswersService : IAnswersService
    {
        private readonly IDeletableEntityRepository<Answer> answersRepository;

        public AnswersService(IDeletableEntityRepository<Answer> answersRepository)
        {
            this.answersRepository = answersRepository;
        }

        public async Task<string> CreateAsync(string questionId, string content, bool isRight)
        {
            var answer = new Answer
            {
                Content = content,
                IsRight = isRight,
                QuestionId = questionId,
            };

            await this.answersRepository.AddAsync(answer);
            await this.answersRepository.SaveChangesAsync();

            return answer.Id;
        }
    }
}
