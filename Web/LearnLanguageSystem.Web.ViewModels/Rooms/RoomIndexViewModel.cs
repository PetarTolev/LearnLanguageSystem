namespace LearnLanguageSystem.Web.ViewModels.Rooms
{
    using System.Collections.Generic;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;
    using LearnLanguageSystem.Web.ViewModels.Users;

    public class RoomIndexViewModel : IMapFrom<Room>
    {
        public string Id { get; set; }

        public int AccessCode { get; set; }

        public string ContestCreatorId { get; set; }

        public string ContestName { get; set; }

        public ICollection<UserInRoomPartialViewModel> Users { get; set; }
    }
}
