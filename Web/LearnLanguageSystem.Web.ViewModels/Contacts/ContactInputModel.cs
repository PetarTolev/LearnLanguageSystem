namespace LearnLanguageSystem.Web.ViewModels.Contacts
{
    using System.ComponentModel.DataAnnotations;

    public class ContactInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
