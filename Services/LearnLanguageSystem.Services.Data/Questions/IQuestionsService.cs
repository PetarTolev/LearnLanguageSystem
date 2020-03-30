namespace LearnLanguageSystem.Services.Data.Questions
{
    using System.Threading.Tasks;

    public interface IQuestionsService
    {
        Task<string> CreateAsync(string contestId, string content);
    }
}
