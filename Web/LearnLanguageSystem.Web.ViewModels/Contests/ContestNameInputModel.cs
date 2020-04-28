namespace LearnLanguageSystem.Web.ViewModels.Contests
{
    using System.ComponentModel.DataAnnotations;

    public class ContestNameInputModel
    {
        [Required(ErrorMessage = ModelConstants.RequiredError)]
        [StringLength(ModelConstants.MaxNameLength, MinimumLength = ModelConstants.MinNameLength, ErrorMessage = ModelConstants.NameLengthError)]
        public string Name { get; set; }
    }
}
