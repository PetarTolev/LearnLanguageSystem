namespace LearnLanguageSystem.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class ChangeAvatarInputModel
    {
        [Required]
        public IFormFile Avatar { get; set; }
    }
}
