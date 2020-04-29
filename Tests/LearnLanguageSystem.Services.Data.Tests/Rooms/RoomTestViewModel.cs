namespace LearnLanguageSystem.Services.Data.Tests.Rooms
{
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class RoomTestViewModel : IMapFrom<Room>
    {
        public string Id { get; set; }

        public string AccessCode { get; set; }

        public string ContestId { get; set; }
    }
}
