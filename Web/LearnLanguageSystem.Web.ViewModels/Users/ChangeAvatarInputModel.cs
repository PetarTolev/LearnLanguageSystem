namespace LearnLanguageSystem.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using LearnLanguageSystem.Web.Infrastructure.Attributes;
    using Microsoft.AspNetCore.Http;

    public class ChangeAvatarInputModel
    {
        [Required]
        [MaxFileSize(4 * 1024 * 1024)]
        [AllowedExtensionsAttribute(new[] { ".jpg", ".png" })]
        public IFormFile Avatar { get; set; }
    }
}
