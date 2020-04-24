namespace LearnLanguageSystem.Web.ViewModels.Rooms
{
    public class AddUserToRoomViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string AvatarUrl { get; set; }

        public string RoomId { get; set; }

        public bool IsForOwner { get; set; }
    }
}
