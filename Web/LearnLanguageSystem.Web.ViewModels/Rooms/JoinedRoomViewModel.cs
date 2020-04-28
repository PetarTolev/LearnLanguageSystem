namespace LearnLanguageSystem.Web.ViewModels.Rooms
{
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class JoinedRoomViewModel : IMapFrom<Room>
    {
        public string Id { get; set; }

        public string ContestName { get; set; }

        public string UsersCount { get; set; }

        public string ContestCreatorUsername { get; set; }
    }
}
