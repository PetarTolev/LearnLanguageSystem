namespace LearnLanguageSystem.Services.Data.Contests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Web.ViewModels.Rooms;

    public interface IContestsService
    {
        T GetById<T>(string contestId);

        T GetQuestions<T>(string id);

        string GetCreatorId(string contestId);

        IEnumerable<T> GetOwned<T>(string userId);

        IEnumerable<Contest> GetOwned(string userId);

        int GetQuestionsCount(string id);

        Task<string> CreateAsync(string name, string contestId);

        Task<bool> ChangeNameAsync(string id, string newName);

        Task<bool> DeleteAsync(string id);
    }
}
