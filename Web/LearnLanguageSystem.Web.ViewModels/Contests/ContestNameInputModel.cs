namespace LearnLanguageSystem.Web.ViewModels.Contests
{
    using System.ComponentModel.DataAnnotations;

    public class ContestNameInputModel
    {
        [Required]
        [StringLength(ModelConstants.Contests.MaxLength, MinimumLength = ModelConstants.Contests.MinLength, ErrorMessage = ModelConstants.NameLengthError)]
        public string Name { get; set; }
    }
}
