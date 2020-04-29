namespace LearnLanguageSystem.Services.Data.Tests.Contacts
{
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.Contacts;
    using LearnLanguageSystem.Services.Mapping;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class ContactsServiceTests : BaseServiceTests
    {
        private const int ContactId = 1;

        public ContactsServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ContactTestViewModel).GetTypeInfo().Assembly);

            this.Contact = new EmailContact
            {
                Id = ContactId,
                Name = "TestName",
                Title = "TestTitle",
                Email = "TestEmail",
                Content = "TestContent",
            };

            this.DbContext.EmailContacts.Add(this.Contact);
            this.DbContext.SaveChanges();
        }

        private EmailContact Contact { get; set; }

        private IContactsService Service => this.ServiceProvider.GetRequiredService<IContactsService>();

        [Fact]
        public async Task AddContactAsyncShouldReturnTrueAndAddContact()
        {
            var result = await this.Service.AddContactAsync("NewName", "NewEmail", "NewTitle", "NewContent");

            var contact = this.DbContext.EmailContacts.First(x => x.Name == "NewName");

            Assert.True(result);
            Assert.NotNull(contact);
        }

        [Fact]
        public void GetAllShouldReturnCorrectCount()
        {
            var result = this.Service.GetAll<ContactTestViewModel>();

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public void GetByIdShouldReturnCorrectType()
        {
            var result = this.Service.GetById<ContactTestViewModel>(ContactId);

            Assert.Equal("TestTitle", result.Title);
        }
    }
}
