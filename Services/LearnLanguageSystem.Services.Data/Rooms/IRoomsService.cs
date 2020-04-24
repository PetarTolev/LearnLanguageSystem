namespace LearnLanguageSystem.Services.Data.Rooms
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Models;

    public interface IRoomsService
    {
        Room GetByCode(int code);

        T GetById<T>(string id);

        string GetOwnerId(string id);

        ICollection<string> GetUsersInIds(string id);

        bool IsExistRoomWithThisContest(string contestId);

        Task<string> OpenAsync(string contestId);

        Task<bool> AddUserAsync(string roomId, ApplicationUser user);

        Task<bool> RemoveUserAsync(string roomId, ApplicationUser user);

        Task<string> CloseAsync(string contestId);
    }
}
