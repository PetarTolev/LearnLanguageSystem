namespace LearnLanguageSystem.Web.ViewModels.Rooms
{
    using System.Collections.Generic;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class RoomPlayViewModel : IMapFrom<Room>
    {
        public ContestViewModel Contest { get; set; }
    }
}
