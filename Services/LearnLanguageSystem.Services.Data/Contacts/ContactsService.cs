namespace LearnLanguageSystem.Services.Data.Contacts
{
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Common.Repositories;
    using LearnLanguageSystem.Data.Models;

    public class ContactsService : IContactsService
    {
        private readonly IRepository<EmailContact> emailContactRepository;

        public ContactsService(IRepository<EmailContact> emailContactRepository)
        {
            this.emailContactRepository = emailContactRepository;
        }

        public async Task AddContactAsync(string name, string email, string title, string content)
        {
            var emailContact = new EmailContact
            {
                Name = name,
                Email = email,
                Title = title,
                Content = content,
            };

            await this.emailContactRepository.AddAsync(emailContact);
            await this.emailContactRepository.SaveChangesAsync();
        }
    }
}
