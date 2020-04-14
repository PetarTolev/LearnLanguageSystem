namespace LearnLanguageSystem.Services.Data.Contests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Common.Repositories;
    using LearnLanguageSystem.Data.Models.Contest;
    using LearnLanguageSystem.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class ContestsService : IContestsService
    {
        private readonly IDeletableEntityRepository<Contest> contestsRepository;
        private readonly IDeletableEntityRepository<Question> questionRepository;
        private readonly IDeletableEntityRepository<Answer> answerRepository;
        private readonly IConfiguration configuration;
        private readonly Random rnd;

        public ContestsService(
            IDeletableEntityRepository<Contest> contestsRepository,
            IDeletableEntityRepository<Question> questionRepository,
            IDeletableEntityRepository<Answer> answerRepository,
            IConfiguration configuration,
            Random rnd)
        {
            this.contestsRepository = contestsRepository;
            this.questionRepository = questionRepository;
            this.answerRepository = answerRepository;
            this.configuration = configuration;
            this.rnd = rnd;
        }

        public T GetById<T>(string contestId)
            => this.contestsRepository
                .AllAsNoTracking()
                .Where(x => x.Id == contestId)
                .To<T>()
                .FirstOrDefault();

        public T GetByKey<T>(string key)
            => this.contestsRepository
                .All()
                .Where(x => x.AccessCode == key)
                .To<T>()
                .FirstOrDefault();

        public string GetCreatorId(string contestId)
            => this.contestsRepository
                .AllAsNoTracking()
                .Where(x => x.Id == contestId)
                .Select(x => x.CreatorId)
                .FirstOrDefault();

        public IEnumerable<T> GetOwned<T>(string userId)
            => this.contestsRepository
                .AllAsNoTracking()
                .Where(c => c.CreatorId == userId)
                .OrderBy(x => x.Name)
                .To<T>()
                .ToList();

        public int GetQuestionsCount(string id)
            => this.contestsRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => x.Questions.Count)
                .FirstOrDefault();

        public async Task<string> CreateAsync(string name, string userId)
        {
            var contest = new Contest
            {
                CreatorId = userId,
                Name = name,
            };

            await this.contestsRepository.AddAsync(contest);
            await this.contestsRepository.SaveChangesAsync();

            return contest.Id;
        }

        public async Task<string> ChangeNameAsync(string id, string newName)
        {
            var contest = this.contestsRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            if (contest == null)
            {
                return null;
            }

            contest.Name = newName;

            this.contestsRepository.Update(contest);
            await this.contestsRepository.SaveChangesAsync();

            return contest.Id;
        }

        public async Task<string> DeleteAsync(string id)
        {
            var contest = await this.contestsRepository
                .All()
                .Where(c => c.Id == id)
                .Include(c => c.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync();

            if (contest == null)
            {
                return null;
            }

            foreach (var question in contest.Questions)
            {
                this.questionRepository.HardDelete(question);

                foreach (var answer in question.Answers)
                {
                    this.answerRepository.HardDelete(answer);
                }
            }

            this.contestsRepository.HardDelete(contest);
            await this.contestsRepository.SaveChangesAsync();

            return contest.Id;
        }

        public async Task<string> OpenAsync(string id)
        {
            var contest = this.contestsRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            if (contest == null || contest.IsOpen)
            {
                return null;
            }

            var existingCodes = this.contestsRepository
                .All()
                .Where(x => x.AccessCode != null)
                .Select(x => x.AccessCode)
                .ToList();

            var codeLength = int.Parse(this.configuration["AccessCodeLength"]);
            var code = this.GenerateCode(codeLength);

            while (existingCodes.Contains(code))
            {
                code = this.GenerateCode(codeLength);
            }

            contest.AccessCode = code;
            contest.IsOpen = true;

            this.contestsRepository.Update(contest);
            await this.contestsRepository.SaveChangesAsync();

            return code;
        }

        public async Task<string> CloseAsync(string id)
        {
            var contest = this.contestsRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            if (contest == null || !contest.IsOpen)
            {
                return null;
            }

            contest.IsOpen = false;
            contest.AccessCode = null;

            this.contestsRepository.Update(contest);
            await this.contestsRepository.SaveChangesAsync();

            return contest.Id;
        }

        private string GenerateCode(int length)
            => this.rnd
                .Next(0, int.MaxValue)
                .ToString()
                .Substring(0, length);
    }
}
