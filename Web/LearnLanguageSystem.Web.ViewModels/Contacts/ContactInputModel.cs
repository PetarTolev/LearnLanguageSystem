namespace LearnLanguageSystem.Web.ViewModels.Contacts
{
    using System.ComponentModel.DataAnnotations;

    public class ContactInputModel
    {
        [Required(ErrorMessage = ModelConstants.RequiredError)]
        [StringLength(ModelConstants.MaxNameLength, MinimumLength = ModelConstants.MinNameLength, ErrorMessage = ModelConstants.NameLengthError)]
        public string Name { get; set; }

        [Required(ErrorMessage = ModelConstants.RequiredError)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = ModelConstants.RequiredError)]
        [StringLength(ModelConstants.Contacts.MaxTitleLength, MinimumLength = ModelConstants.Contacts.MinTitleLength, ErrorMessage = ModelConstants.Contacts.TitleLengthError)]
        public string Title { get; set; }

        [Required(ErrorMessage = ModelConstants.RequiredError)]
        [StringLength(ModelConstants.Contacts.MaxContentLength, MinimumLength = ModelConstants.Contacts.MinContentLength, ErrorMessage = ModelConstants.Contacts.ContentLengthError)]
        public string Content { get; set; }
    }
}
