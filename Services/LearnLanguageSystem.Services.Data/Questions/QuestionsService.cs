namespace LearnLanguageSystem.Services.Data.Questions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Common.Repositories;
    using LearnLanguageSystem.Data.Models.Contest;
    using LearnLanguageSystem.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class QuestionsService : IQuestionsService
    {
        private readonly IDeletableEntityRepository<Question> questionRepository;

        public QuestionsService(IDeletableEntityRepository<Question> questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public async Task<string> CreateAsync(string contestId, string content)
        {
            var question = new Question
            {
                ContestId = contestId,
                Content = content,
            };

            await this.questionRepository.AddAsync(question);
            await this.questionRepository.SaveChangesAsync();

            return question.Id;
        }

        public async Task<T> GetByIdAsync<T>(string id)
        {
            var question = await this.questionRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            if (question == null)
            {
                throw new NullReferenceException();
            }

            return question;
        }

        public async Task<string> DeleteAsync(string id)
        {
            var question = await this.GetWithAnswer(id);

            this.questionRepository.HardDelete(question);
            await this.questionRepository.SaveChangesAsync();

            return question.ContestId;
        }

        public async Task<Question> GetWithAnswer(string id)
        {
            var question = await this.questionRepository
                .All()
                .Include(x => x.Answers)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (question == null)
            {
                throw new NullReferenceException();
            }

            return question;
        }
    }
}
