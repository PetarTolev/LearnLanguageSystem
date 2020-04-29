namespace LearnLanguageSystem.Services.Data.Tests
{
    using System;
    using System.IO;

    using AutoMapper;
    using CloudinaryDotNet;
    using LearnLanguageSystem.Data;
    using LearnLanguageSystem.Data.Common;
    using LearnLanguageSystem.Data.Common.Repositories;
    using LearnLanguageSystem.Data.Repositories;
    using LearnLanguageSystem.Services.Data.Answers;
    using LearnLanguageSystem.Services.Data.ApplicationSettings;
    using LearnLanguageSystem.Services.Data.Cloud;
    using LearnLanguageSystem.Services.Data.Contacts;
    using LearnLanguageSystem.Services.Data.Contests;
    using LearnLanguageSystem.Services.Data.Questions;
    using LearnLanguageSystem.Services.Data.Rooms;
    using LearnLanguageSystem.Services.Messaging;
    using LearnLanguageSystem.Web;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class BaseServiceTests : IDisposable
    {
        protected BaseServiceTests()
        {
            this.Configuration = this.SetConfiguration();
            var services = this.SetServices();
            this.ServiceProvider = services.BuildServiceProvider();
            this.DbContext = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }

        protected IServiceProvider ServiceProvider { get; set; }

        protected ApplicationDbContext DbContext { get; set; }

        protected IConfigurationRoot Configuration { get; set; }

        public void Dispose()
        {
            this.DbContext.Database.EnsureDeleted();
            this.SetServices();
        }

        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services
                .Configure<IdentityOptions>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;

                    options.User.RequireUniqueEmail = false;
                });

            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton<IConfiguration>(this.Configuration);

            Account account = new Account(
                this.Configuration["CloudinarySettings:CloudName"],
                this.Configuration["CloudinarySettings:ApiKey"],
                this.Configuration["CloudinarySettings:ApiSecret"]);

            var cloudinary = new Cloudinary(account);
            services.AddSingleton(cloudinary);

            services
                .AddSingleton<IConfiguration>(this.Configuration)
                .AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>))
                .AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
                .AddScoped<IDbQueryRunner, DbQueryRunner>()
                .AddScoped<Random>()
                .AddTransient<IEmailSender>(x => new SendGridEmailSender(this.Configuration["SendGrid:ApiKey"]))
                .AddTransient<IEmailSender, NullMessageSender>()
                .AddTransient<IContestsService, ContestsService>()
                .AddTransient<IQuestionsService, QuestionsService>()
                .AddTransient<IAnswersService, AnswersService>()
                .AddTransient<IRoomsService, RoomsService>()
                .AddTransient<IApplicationSettingsService, ApplicationSettingsService>()
                .AddTransient<ICloudinaryService, CloudinaryService>()
                .AddTransient<IContactsService, ContactsService>();

            var context = new DefaultHttpContext();
            services.AddSingleton<IHttpContextAccessor>(new HttpContextAccessor { HttpContext = context });

            return services;
        }

        private IConfigurationRoot SetConfiguration()
            => new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath("../../../"))
                .AddJsonFile(
                    path: "appsettings.json",
                    optional: false,
                    reloadOnChange: true)
                .Build();
    }
}
