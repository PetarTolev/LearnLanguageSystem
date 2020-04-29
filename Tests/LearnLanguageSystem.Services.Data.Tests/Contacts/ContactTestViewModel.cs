namespace LearnLanguageSystem.Services.Data.Tests.Contacts
{
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class ContactTestViewModel : IMapFrom<EmailContact>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
