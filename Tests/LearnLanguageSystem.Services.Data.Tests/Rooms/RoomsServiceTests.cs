namespace LearnLanguageSystem.Services.Data.Tests.Rooms
{
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.Rooms;
    using LearnLanguageSystem.Services.Mapping;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class RoomsServiceTests : BaseServiceTests
    {
        private const string RoomId = "84261368-f72f-4d44-ad06-d714abf40fbc";
        private const string ContestId = "0e9f9726-b07e-4fa8-95be-72d754b772f3";
        private const string UserInId = "89f6c729-481b-4872-88d5-1f7ef3b7e6c1";

        public RoomsServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(RoomTestViewModel).GetTypeInfo().Assembly);

            var creator = new ApplicationUser
            {
                Id = "25b7fd18-13fa-4e0f-acb9-c0cdd9dc144d",
                UserName = "TestCreatorUsername",
                AvatarUrl = "TestCreatorAvatarUrl",
                Email = "TestCreatorEmail",
            };

            var userIn = new ApplicationUser
            {
                Id = UserInId,
                UserName = "TestUserUsername",
                AvatarUrl = "TestUserAvatarUrl",
                Email = "TestUserEmail",
            };

            var answer = new Answer
            {
                Id = "73709ae5-8e39-4c6c-913f-2dd07809ba33",
                IsRight = true,
                Content = "TestAnswerContent",
            };

            var question = new Question
            {
                Id = "287d606e-f114-46fe-8d17-42129292b5c3",
                Content = "TestAnswerContent",
            };

            question.Answers.Add(answer);

            var contest = new Contest
            {
                Id = ContestId,
                Name = "TestContestName",
                Creator = creator,
            };

            contest.Questions.Add(question);

            this.Room = new Room
            {
                Id = RoomId,
                Contest = contest,
                AccessCode = 1234,
            };

            this.Room.Users.Add(userIn);

            var applicationSettings = new ApplicationSettings
            {
                Id = 1,
                AccessCodeLength = 4,
            };

            this.DbContext.ApplicationSettings.Add(applicationSettings);
            this.DbContext.Rooms.Add(this.Room);
            this.DbContext.SaveChanges();
        }

        private IRoomsService Service => this.ServiceProvider.GetRequiredService<IRoomsService>();

        private Room Room { get; set; }

        [Fact]
        public void GetAllShouldReturnCorrectCount()
        {
            var result = this.Service.GetAll<RoomTestViewModel>();

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public void GetByCodeShouldReturnCorrectRoom()
        {
            var code = 1234;

            var result = this.Service.GetByCode(code);

            Assert.Equal(this.Room.Id, result.Id);
        }

        [Fact]
        public void GetByIdShouldReturnCorrectType()
        {
            var result = this.Service.GetById<RoomTestViewModel>(RoomId);

            Assert.IsType<RoomTestViewModel>(result);
        }

        [Fact]
        public void GetByIdShouldReturnNullWhenRoomNotExist()
        {
            var roomId = "notExist";

            var result = this.Service.GetById<RoomTestViewModel>(roomId);

            Assert.Null(result);
        }

        [Fact]
        public void GetOwnerIdShouldReturnCorrectId()
        {
            var result = this.Service.GetOwnerId(RoomId);

            Assert.Equal(this.Room.Contest.CreatorId, result);
        }

        [Fact]
        public void GetUsersInShouldReturnCorrectCountUsers()
        {
            var result = this.Service.GetUsersInIds(RoomId);

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public void IsExistRoomWithThisContestShouldReturnCorrectResult()
        {
            var result = this.Service.IsExistRoomWithThisContest(ContestId);

            Assert.True(result);
        }

        [Fact]
        public async Task OpenAsyncShouldReturnNullWhenContestNotExist()
        {
            var contestId = "notExist";

            var roomId = await this.Service.OpenAsync(contestId);

            Assert.Null(roomId);
        }

        [Fact]
        public async Task OpenAsyncShouldCreateRoom()
        {
            var result = await this.Service.OpenAsync(ContestId);

            var contest = this.DbContext.Contests.First(x => x.Id == ContestId);

            Assert.Equal(contest.Room.Id, result);
        }

        [Fact]
        public async Task AddUserAsyncShouldReturnTrueWhenRoomExist()
        {
            var user = new ApplicationUser();

            var result = await this.Service.AddUserAsync(RoomId, user);

            Assert.True(result);
            Assert.Equal(2, this.Room.Users.Count);
        }

        [Fact]
        public async Task AddUserAsyncShouldReturnFalseWhenRoomNotExist()
        {
            var roomId = "NotExist";
            var user = new ApplicationUser();

            var result = await this.Service.AddUserAsync(roomId, user);

            Assert.False(result);
            Assert.Equal(1, this.Room.Users.Count);
        }

        [Fact]
        public async Task RemoveUserAsyncShouldReturnTrueAndRemoveUser()
        {
            var user = this.DbContext.Users.First(x => x.Id == UserInId);

            var result = await this.Service.RemoveUserAsync(RoomId, user);

            Assert.True(result);
            Assert.Equal(0, this.Room.Users.Count);
        }

        [Fact]
        public async Task RemoveUserAsyncShouldReturnFalseWhenRoomNotExist()
        {
            var roomId = "NotExist";
            var user = new ApplicationUser();

            var result = await this.Service.RemoveUserAsync(roomId, user);

            Assert.False(result);
            Assert.Equal(1, this.Room.Users.Count);
        }

        [Fact]
        public async Task CloseAsyncShouldReturnFalseWhenRoomNotExist()
        {
            var roomId = "NotExist";

            var result = await this.Service.CloseAsync(roomId);

            Assert.False(result);
            Assert.Equal(1, this.DbContext.Rooms.Count());
        }

        [Fact]
        public async Task CloseAsyncShouldReturnTrueAndCloseRoom()
        {
            var result = await this.Service.CloseAsync(RoomId);

            Assert.True(result);
            Assert.Equal(0, this.DbContext.Rooms.Count());
        }
    }
}
