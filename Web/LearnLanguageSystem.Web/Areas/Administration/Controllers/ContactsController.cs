namespace LearnLanguageSystem.Web.Areas.Administration.Controllers
{
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

        public IActionResult Details(int id)
        {
            var model = this.contactsService.GetById<ContactViewModel>(id);

            if (model == null)
            {
                return this.RedirectToAction("BadRequest", "Errors");
            }

            return this.View(model);
        }
    }
}
