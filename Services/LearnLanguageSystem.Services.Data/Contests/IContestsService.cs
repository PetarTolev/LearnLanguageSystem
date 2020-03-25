namespace LearnLanguageSystem.Services.Data.Contests
{
    using System.Collections.Generic;

    public interface IContestsService
    {
        IEnumerable<T> GetCreatedContests<T>(string userId);
    }
}
