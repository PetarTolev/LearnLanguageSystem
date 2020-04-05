namespace LearnLanguageSystem.Services.Data.Contests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IContestsService
    {
        Task<T> GetByIdAsync<T>(string contestId);

        Task<T> GetByKeyAsync<T>(string key);

        Task<IEnumerable<T>> GetOwnedAsync<T>(string userId);

        Task<string> CreateAsync(string name, string contestId);

        Task ChangeNameAsync(string id, string newName);

        Task DeleteAsync(string id);

        Task<string> OpenAsync(string id);
    }
}
