namespace LearnLanguageSystem.Web.ViewModels.Users
{
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class UserInRoomPartialViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string AvatarUrl { get; set; }

        public string RoomId { get; set; }

        public string CurrentUserId { get; set; }

        public string ContestCreatorId { get; set; }

        public bool IsOwner => this.ContestCreatorId == this.CurrentUserId;

        public bool IsMe => this.Id == this.CurrentUserId;
    }
}
