namespace LearnLanguageSystem.Services.Data.Questions
{
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Models.Contest;
    using LearnLanguageSystem.Web.ViewModels.Questions;

    public interface IQuestionsService
    {
        Task<string> CreateAsync(string contestId, string content);

        Task<T> GetByIdAsync<T>(string id);

        Task<string> GetCreatorId(string id);

        Task<string> UpdateAsync(QuestionEditViewModel model);

        Task<string> DeleteAsync(string id);

        Task<Question> GetWithAnswerAsync(string id);
    }
}
