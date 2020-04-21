namespace LearnLanguageSystem.Web.ViewModels.Users
{
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class UserInRoomViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string AvatarUrl { get; set; }
    }
}
