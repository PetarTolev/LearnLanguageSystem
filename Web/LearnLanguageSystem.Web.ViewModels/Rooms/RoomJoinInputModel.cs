namespace LearnLanguageSystem.Web.ViewModels.Rooms
{
    using System.ComponentModel.DataAnnotations;

    public class RoomJoinInputModel
    {
        [Required]
        public int AccessCode { get; set; }
    }
}
