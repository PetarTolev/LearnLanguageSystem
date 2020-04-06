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

        public ContestsService(
            IDeletableEntityRepository<Contest> contestsRepository,
            IDeletableEntityRepository<Question> questionRepository,
            IDeletableEntityRepository<Answer> answerRepository,
            IConfiguration configuration)
        {
            this.contestsRepository = contestsRepository;
            this.questionRepository = questionRepository;
            this.answerRepository = answerRepository;
            this.configuration = configuration;
        }

        public async Task<T> GetByIdAsync<T>(string contestId)
        {
            var contest = await this.contestsRepository
                .All()
                .Where(x => x.Id == contestId)
                .To<T>()
                .FirstOrDefaultAsync();

            if (contest == null)
            {
                throw new NullReferenceException();
            }

            return contest;
        }

        public async Task<T> GetByKeyAsync<T>(string key)
        {
            var contest = await this.contestsRepository
                .All()
                .Where(x => x.AccessCode == key)
                .To<T>()
                .FirstOrDefaultAsync();

            if (contest == null)
            {
                throw new NullReferenceException();
            }

            return contest;
        }

        public Task<string> GetCreatorId(string contestId)
        {
            var creatorId = this.contestsRepository
                .All()
                .Where(x => x.Id == contestId)
                .Select(x => x.CreatorId)
                .FirstOrDefaultAsync();

            if (creatorId == null)
            {
                throw new NullReferenceException();
            }

            return creatorId;
        }

        public async Task<IEnumerable<T>> GetOwnedAsync<T>(string userId)
        {
            var contests = await this.contestsRepository
                .All()
                .Where(c => c.CreatorId == userId)
                .To<T>()
                .ToListAsync();

            if (contests == null)
            {
                throw new NullReferenceException();
            }

            return contests;
        }

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

        public async Task ChangeNameAsync(string id, string newName)
        {
            var contest = await this.contestsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (contest == null)
            {
                throw new NullReferenceException();
            }

            contest.Name = newName;

            await this.contestsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var contest = await this.contestsRepository
                .All()
                .Where(c => c.Id == id)
                .Include(c => c.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync();

            if (contest == null)
            {
                throw new NullReferenceException();
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
        }

        public async Task<string> OpenAsync(string id)
        {
            var contest = await this.contestsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (contest == null)
            {
                throw new NullReferenceException();
            }

            if (contest.IsOpen)
            {
                throw new ArgumentException();
            }

            var existingCodes = await this.contestsRepository
                .All()
                .Where(x => x.AccessCode != null)
                .Select(x => x.AccessCode)
                .ToListAsync();

            var code = this.GenerateCode();

            while (existingCodes.Contains(code))
            {
                code = this.GenerateCode();
            }

            contest.AccessCode = code;
            contest.IsOpen = true;

            this.contestsRepository.Update(contest);
            await this.contestsRepository.SaveChangesAsync();

            return code;
        }

        public async Task Close(string id)
        {
            var contest = await this.contestsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (contest == null)
            {
                throw new NullReferenceException();
            }

            contest.IsOpen = false;
            contest.AccessCode = null;

            this.contestsRepository.Update(contest);
            await this.contestsRepository.SaveChangesAsync();
        }

        private string GenerateCode()
        {
            var codeLength = int.Parse(this.configuration["AccessCodeLength"]);

            var rnd = new Random();
            var number = rnd
                .Next(0, int.MaxValue)
                .ToString()
                .Substring(0, codeLength);

            return number;
        }
    }
}
