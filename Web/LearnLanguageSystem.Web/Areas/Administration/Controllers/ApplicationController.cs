namespace LearnLanguageSystem.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using LearnLanguageSystem.Services.Data.ApplicationSettings;
    using LearnLanguageSystem.Web.ViewModels.Administration.Application;
    using Microsoft.AspNetCore.Mvc;

    public class ApplicationController : AdministrationController
    {
        private readonly IApplicationSettingsService applicationSettingsService;

        public ApplicationController(IApplicationSettingsService applicationSettingsService)
        {
            this.applicationSettingsService = applicationSettingsService;
        }

        public IActionResult Settings()
        {
            var codeLength = this.applicationSettingsService.GetAccessCodeLength();

            var model = new SettingsViewModel { AccessCodeLength = codeLength };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Settings(SettingsViewModel model)
        {
            var isSuccessfully = await this.applicationSettingsService.ChangeAccessCodeLength(model.AccessCodeLength);

            if (!isSuccessfully)
            {
                return this.RedirectToAction("BadRequest", "Errors");
            }

            return this.RedirectToAction(nameof(this.Settings));
        }
    }
}
