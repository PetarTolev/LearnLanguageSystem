namespace LearnLanguageSystem.Web.ViewModels.Administration.Application
{
    using System.ComponentModel.DataAnnotations;

    public class SettingsViewModel
    {
        [Required(ErrorMessage = ModelConstants.RequiredError)]
        public int AccessCodeLength { get; set; }
    }
}
