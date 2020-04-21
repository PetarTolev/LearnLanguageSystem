namespace LearnLanguageSystem.Services.Data.Rooms
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Common.Repositories;
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.ApplicationSettings;
    using LearnLanguageSystem.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class RoomsService : IRoomsService
    {
        private readonly IRepository<Room> roomsRepository;
        private readonly IRepository<Contest> contestsRepository;
        private readonly IApplicationSettingsService applicationSettingsService;
        private readonly Random rnd;

        public RoomsService(IRepository<Room> roomsRepository, IRepository<Contest> contestsRepository, IApplicationSettingsService applicationSettingsService, Random rnd)
        {
            this.roomsRepository = roomsRepository;
            this.contestsRepository = contestsRepository;
            this.applicationSettingsService = applicationSettingsService;
            this.rnd = rnd;
        }

        public Room GetByCode(int code)
            => this.roomsRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.AccessCode == code);

        public T GetById<T>(string id)
            => this.roomsRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

        public string GetOwnerId(string id)
            => this.roomsRepository
                .All()
                .Where(x => x.Id == id)
                .Select(x => x.Contest.CreatorId)
                .FirstOrDefault();

        public bool IsExistRoomWithThisContest(string contestId)
            => this.roomsRepository
                .AllAsNoTracking()
                .Any(x => x.ContestId == contestId);

        public async Task<string> OpenAsync(string contestId)
        {
            var contest = this.contestsRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Id == contestId);

            if (contest == null)
            {
                return null;
            }

            var room = await this.CreateRoom(contest);

            return room.Id;
        }

        public async Task<bool> AddUserAsync(string roomId, ApplicationUser user)
        {
            var room = this.roomsRepository
                .All()
                .FirstOrDefault(x => x.Id == roomId);

            if (room == null)
            {
                return false;
            }

            room.Users.Add(user);
            this.roomsRepository.Update(room);
            await this.roomsRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveUserAsync(string roomId, ApplicationUser user)
        {
            var room = this.roomsRepository
                .All()
                .FirstOrDefault(x => x.Id == roomId);

            if (room == null)
            {
                return false;
            }

            room.Users.Remove(user);
            this.roomsRepository.Update(room);
            await this.roomsRepository.SaveChangesAsync();

            return true;
        }

        public async Task<string> CloseAsync(string contestId)
        {
            var room = this.roomsRepository
                .All()
                .Include(x => x.Users)
                .FirstOrDefault(x => x.ContestId == contestId);

            if (room == null)
            {
                return null;
            }

            this.roomsRepository.Delete(room);
            await this.roomsRepository.SaveChangesAsync();

            return room.Id;
        }

        private async Task<Room> CreateRoom(Contest contest)
        {
            var existingCodes = this.roomsRepository
                .All()
                .Select(x => x.AccessCode)
                .ToList();

            var codeLength = this.applicationSettingsService.GetAccessCodeLength();

            var code = this.GenerateCode(codeLength);

            while (existingCodes.Contains(code))
            {
                code = this.GenerateCode(codeLength);
            }

            var room = new Room
            {
                AccessCode = code,
                ContestId = contest.Id,
            };

            await this.roomsRepository.AddAsync(room);
            await this.roomsRepository.SaveChangesAsync();

            return room;
        }

        private int GenerateCode(int length)
            => int.Parse(this.rnd
                .Next(0, int.MaxValue)
                .ToString()
                .Substring(0, length));
    }
}
