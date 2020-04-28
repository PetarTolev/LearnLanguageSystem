namespace LearnLanguageSystem.Services.Data.Answers
{
    using System.Threading.Tasks;

    public interface IAnswersService
    {
        Task<bool> CreateAsync(string questionId, string content, bool isRight);
    }
}
