namespace LearnLanguageSystem.Services.Data.Contests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Models.Contest;

    public interface IContestsService
    {
        T GetById<T>(string contestId);

        T GetByKey<T>(string key);

        string GetCreatorId(string contestId);

        IEnumerable<T> GetOwned<T>(string userId);

        IEnumerable<Contest> GetOwned(string userId);

        int GetQuestionsCount(string id);

        Task<string> CreateAsync(string name, string contestId);

        Task<string> ChangeNameAsync(string id, string newName);

        Task<string> DeleteAsync(string id);

        Task<string> OpenAsync(string id);

        Task<string> CloseAsync(string id);
    }
}
