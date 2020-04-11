namespace LearnLanguageSystem.Services.Data.Questions
{
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Models.Contest;
    using LearnLanguageSystem.Web.ViewModels.Questions;

    public interface IQuestionsService
    {
        Task<string> CreateAsync(string contestId, string content);

        T GetById<T>(string id);

        string GetCreatorId(string id);

        Task<string> UpdateAsync(QuestionEditViewModel model);

        Task<string> DeleteAsync(string id);
    }
}
