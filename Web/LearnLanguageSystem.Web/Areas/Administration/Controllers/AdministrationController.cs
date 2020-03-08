namespace LearnLanguageSystem.Web.Areas.Administration.Controllers
{
    using LearnLanguageSystem.Common;
    using LearnLanguageSystem.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
