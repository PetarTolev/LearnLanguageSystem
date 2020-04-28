namespace LearnLanguageSystem.Web.Areas.Administration.Controllers
{
    using System;

    using LearnLanguageSystem.Services.Data.Contacts;
    using LearnLanguageSystem.Web.ViewModels.Administration.Contacts;
    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : AdministrationController
    {
        private readonly IContactsService contactsService;

        public ContactsController(IContactsService contactsService)
        {
            this.contactsService = contactsService;
        }

        public IActionResult All()
        {
            var model = this.contactsService.GetAll<AllContactsViewModel>();

            return this.View(model);
        }

        public IActionResult Details()
        {
            //todo
            throw new NotImplementedException();
        }
    }
}
