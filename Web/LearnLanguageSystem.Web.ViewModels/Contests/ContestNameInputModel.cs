namespace LearnLanguageSystem.Web.ViewModels.Contests
{
    using System.ComponentModel.DataAnnotations;

    public class ContestNameInputModel
    {
        [Required(ErrorMessage = ModelConstants.RequiredError)]
        [StringLength(ModelConstants.Contests.MaxNameLength, MinimumLength = ModelConstants.Contests.MinNameLength, ErrorMessage = ModelConstants.Contests.NameLengthError)]
        public string Name { get; set; }
    }
}
