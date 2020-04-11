namespace LearnLanguageSystem.Services.Data.Questions
{
    using System.Linq;
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Common.Repositories;
    using LearnLanguageSystem.Data.Models.Contest;
    using LearnLanguageSystem.Services.Mapping;
    using LearnLanguageSystem.Web.ViewModels.Questions;
    using Microsoft.EntityFrameworkCore;

    public class QuestionsService : IQuestionsService
    {
        private readonly IDeletableEntityRepository<Question> questionRepository;
        private readonly IDeletableEntityRepository<Answer> answerRepository;

        public QuestionsService(IDeletableEntityRepository<Question> questionRepository, IDeletableEntityRepository<Answer> answerRepository)
        {
            this.questionRepository = questionRepository;
            this.answerRepository = answerRepository;
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

        public T GetById<T>(string id)
        {
            var question = this.questionRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return question;
        }

        public string GetCreatorId(string id)
        {
            var creatorId = this.questionRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => x.Contest.CreatorId)
                .FirstOrDefault();

            return creatorId;
        }

        public async Task<string> UpdateAsync(QuestionEditViewModel model)
        {
            var question = this.GetWithAnswer(model.Id);

            if (question == null)
            {
                return null;
            }

            question.Content = model.Content;

            var i = 0;
            foreach (var answer in question.Answers)
            {
                answer.Content = model.Answers[i].Content;
                answer.IsRight = model.Answers[i].IsRight;

                i++;
            }

            this.questionRepository.Update(question);
            await this.questionRepository.SaveChangesAsync();

            return question.ContestId;
        }

        public async Task<string> DeleteAsync(string id)
        {
            var question = this.GetWithAnswer(id);

            if (question == null)
            {
                return null;
            }

            foreach (var answer in question.Answers)
            {
                this.answerRepository.HardDelete(answer);
            }

            this.questionRepository.HardDelete(question);
            await this.questionRepository.SaveChangesAsync();

            return question.ContestId;
        }

        private Question GetWithAnswer(string id)
        {
            var question = this.questionRepository
                .All()
                .Include(x => x.Answers)
                .FirstOrDefault(x => x.Id == id);

            return question;
        }
    }
}
