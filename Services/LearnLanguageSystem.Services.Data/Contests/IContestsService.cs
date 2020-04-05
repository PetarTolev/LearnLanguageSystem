namespace LearnLanguageSystem.Services.Data.Contests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IContestsService
    {
        IEnumerable<T> GetOwned<T>(string userId);

        Task<string> CreateAsync(string name, string contestId);

        T GetById<T>(string contestId);

        Task ChangeNameAsync(string id, string newName);

        Task<string> OpenAsync(string id);
    }
}
