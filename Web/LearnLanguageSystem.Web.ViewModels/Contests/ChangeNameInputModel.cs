namespace LearnLanguageSystem.Web.ViewModels.Contests
{
    using System.ComponentModel.DataAnnotations;

    public class ChangeNameInputModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(ModelConstants.Contests.MaxLength, MinimumLength = ModelConstants.Contests.MinLength, ErrorMessage = ModelConstants.NameLengthError)]
        public string Name { get; set; }
    }
}
