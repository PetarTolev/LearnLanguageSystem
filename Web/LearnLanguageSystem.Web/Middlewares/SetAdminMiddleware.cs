namespace LearnLanguageSystem.Web.Middlewares
{
    using System.Linq;
    using System.Threading.Tasks;

    using LearnLanguageSystem.Common;
    using LearnLanguageSystem.Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;

    public class SetAdminMiddleware
    {
        private readonly RequestDelegate next;

        public SetAdminMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            var username = configuration["AdminData:UserName"];
            var email = configuration["AdminData:Email"];
            var password = configuration["AdminData:Password"];

            await SeedUserInRoles(userManager, username, email, password);
            await this.next(context);
        }

        private static async Task SeedUserInRoles(UserManager<ApplicationUser> userManager, string username, string email, string password)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    UserName = username,
                    Email = email,
                };

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
                }
            }
        }
    }
}
