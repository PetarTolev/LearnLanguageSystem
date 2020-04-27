namespace LearnLanguageSystem.Web.ViewModels.Administration.Contacts
{
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class AllContactsViewModel : IMapFrom<EmailContact>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
