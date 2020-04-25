namespace LearnLanguageSystem.Services.Data.Contacts
{
    using System.Threading.Tasks;

    public interface IContactsService
    {
        Task AddContactAsync(string name, string email, string title, string content);
    }
}