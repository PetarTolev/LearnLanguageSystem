namespace LearnLanguageSystem.Services.Data.Contests
{
    using System.Collections.Generic;
    using System.Linq;

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

        public IEnumerable<T> GetCreatedContests<T>(string userId)
        {
            return this.contestsRepository
                .All()
                .Where(c => c.CreatorId == userId)
                .To<T>()
                .ToList();
        }

        public void Create<T>(List<T> fields)
        {
            throw new System.NotImplementedException();
        }
    }
}
