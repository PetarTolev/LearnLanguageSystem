namespace LearnLanguageSystem.Services.Data.ApplicationSettings
{
    using System.Threading.Tasks;

    public interface IApplicationSettingsService
    {
        int GetAccessCodeLength();

        Task<bool> ChangeAccessCodeLength(int newLength);
    }
}
