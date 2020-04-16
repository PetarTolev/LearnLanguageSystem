namespace LearnLanguageSystem.Services.Data.ApplicationSettings
{
    using System.Linq;
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Common.Repositories;
    using LearnLanguageSystem.Data.Models;

    public class ApplicationSettingsService : IApplicationSettingsService
    {
        private readonly IRepository<ApplicationSettings> applicationSettings;

        public ApplicationSettingsService(IRepository<ApplicationSettings> applicationSettings)
        {
            this.applicationSettings = applicationSettings;
        }

        public int GetAccessCodeLength()
            => this.applicationSettings
                   .All()
                   .Where(x => x.Id == 1)
                   .Select(x => x.AccessCodeLength)
                   .FirstOrDefault();

        public async Task ChangeAccessCodeLength(int newLength)
        {
            var applicationSetting = this.applicationSettings
                .All()
                .Where(x => x.Id == 1)
                .FirstOrDefault();

            applicationSetting.AccessCodeLength = newLength;

            this.applicationSettings.Update(applicationSetting);
            await this.applicationSettings.SaveChangesAsync();
        }
    }
}
