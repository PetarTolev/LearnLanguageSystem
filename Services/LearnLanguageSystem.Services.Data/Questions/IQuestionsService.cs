namespace LearnLanguageSystem.Services.Data.Questions
{
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Models.Contest;

    public interface IQuestionsService
    {
        Task<string> CreateAsync(string contestId, string content);

        Task<T> GetByIdAsync<T>(string id);

        Task<string> DeleteAsync(string id);

        Task<Question> GetWithAnswer(string id);
    }
}
