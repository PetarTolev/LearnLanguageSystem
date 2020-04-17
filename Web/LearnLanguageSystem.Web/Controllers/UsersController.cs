using LearnLanguageSystem.Services.Data.Cloudinary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnLanguageSystem.Web.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController()
        {
        }

        public IActionResult ChangeAvatar()
        {
            return this.NoContent();
        }
    }
}
