﻿namespace LearnLanguageSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using LearnLanguageSystem.Services.Data.Contacts;
    using LearnLanguageSystem.Web.Filters;
    using LearnLanguageSystem.Web.ViewModels.Contacts;
    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : BaseController
    {
        private const string Redirected = "RedirectedFromIndex";

        private readonly IContactsService contactsService;

        public ContactsController(IContactsService contactsService)
        {
            this.contactsService = contactsService;
        }

        public IActionResult Index()
            => this.View();

        [HttpPost]
        [ModelStateValidation]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactInputModel model)
        {
            var isSuccessfully = await this.contactsService.AddContactAsync(model.Name, model.Email, model.Title, model.Content);

            if (!isSuccessfully)
            {
                return this.RedirectToAction("BadRequest", "Errors");
            }

            this.TempData[Redirected] = true;
            return this.RedirectToAction(nameof(this.ThankYou), new { name = model.Name });
        }

        public IActionResult ThankYou(string name)
        {
            if (this.TempData[Redirected] == null)
            {
                return this.RedirectToAction("NotFound", "Errors");
            }

            return this.View((object)name);
        }
    }
}
