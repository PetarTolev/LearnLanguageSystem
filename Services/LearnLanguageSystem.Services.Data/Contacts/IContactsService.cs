namespace LearnLanguageSystem.Services.Data.Contacts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IContactsService
    {
        Task AddContactAsync(string name, string email, string title, string content);

        ICollection<T> GetAll<T>();
    }
}
