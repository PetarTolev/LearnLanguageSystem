namespace LearnLanguageSystem.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class RoomDetailsViewModel : IMapFrom<Room>
    {
        public int AccessCode { get; set; }

        public List<string> UsersUsername { get; set; }
    }
}
