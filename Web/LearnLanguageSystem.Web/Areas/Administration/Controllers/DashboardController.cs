namespace LearnLanguageSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;

    using LearnLanguageSystem.Data.Common.Repositories;
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;
    using LearnLanguageSystem.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly IRepository<Contest> contestsRepository;
        private readonly IRepository<Room> roomsRepository;

        public DashboardController(
            IRepository<Contest> contestsRepository,
            IRepository<Room> roomsRepository)
        {
            this.contestsRepository = contestsRepository;
            this.roomsRepository = roomsRepository;
        }

        public IActionResult Contests()
        {
            var contests = this.contestsRepository
                .AllAsNoTracking()
                .To<ContestViewModel>()
                .ToList();

            return this.View(contests);
        }

        public IActionResult RoomDetails(string roomId)
        {
            var room = this.roomsRepository
                .AllAsNoTracking()
                .Where(x => x.Id == roomId)
                .Select(x => new
                    RoomDetailsViewModel
                    {
                        AccessCode = x.AccessCode,
                        UsersUsername = x.Users.Select(u => u.UserName).ToList(),
                    })
                .FirstOrDefault();

            return this.View(room);
        }
    }
}
