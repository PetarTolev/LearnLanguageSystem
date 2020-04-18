namespace LearnLanguageSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.Cloud;
    using LearnLanguageSystem.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class UsersController : BaseController
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(ICloudinaryService cloudinaryService, UserManager<ApplicationUser> userManager)
        {
            this.cloudinaryService = cloudinaryService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> ChangeAvatar(ChangeAvatarInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.NotFound();
            }

            var url = await this.cloudinaryService.Upload(model.Avatar);

            var user = await this.userManager.GetUserAsync(this.User);

            user.AvatarUrl = url;
            await this.userManager.UpdateAsync(user);

            return this.Redirect("/Identity/Account/Manage");
        }
    }
}
