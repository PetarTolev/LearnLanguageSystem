namespace LearnLanguageSystem.Services.Data.Contests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Common.Repositories;
    using LearnLanguageSystem.Data.Models.Contest;
    using LearnLanguageSystem.Services.Mapping;

    public class ContestsService : IContestsService
    {
        private readonly IDeletableEntityRepository<Contest> contestsRepository;

        public ContestsService(IDeletableEntityRepository<Contest> contestsRepository)
        {
            this.contestsRepository = contestsRepository;
        }

        public IEnumerable<T> GetOwned<T>(string userId)
        {
            var contests = this.contestsRepository
                .All()
                .Where(c => c.CreatorId == userId)
                .To<T>()
                .ToList();

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

        public T GetById<T>(string contestId)
        {
            var contest = this.contestsRepository.All().Where(x => x.Id == contestId).To<T>().FirstOrDefault();

            return contest;
        }

        public async Task ChangeNameAsync(string id, string newName)
        {
            var contest = this.contestsRepository.All().FirstOrDefault(x => x.Id == id);

            contest.Name = newName;

            await this.contestsRepository.SaveChangesAsync();
        }
    }
}
