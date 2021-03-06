﻿namespace LearnLanguageSystem.Web.Infrastructure
{
    using System;

    using CloudinaryDotNet;
    using LearnLanguageSystem.Data;
    using LearnLanguageSystem.Data.Common;
    using LearnLanguageSystem.Data.Common.Repositories;
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Data.Repositories;
    using LearnLanguageSystem.Services.Data.Answers;
    using LearnLanguageSystem.Services.Data.ApplicationSettings;
    using LearnLanguageSystem.Services.Data.Cloud;
    using LearnLanguageSystem.Services.Data.Contacts;
    using LearnLanguageSystem.Services.Data.Contests;
    using LearnLanguageSystem.Services.Data.Questions;
    using LearnLanguageSystem.Services.Data.Rooms;
    using LearnLanguageSystem.Services.Messaging;
    using LearnLanguageSystem.Web.Filters;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddDbContext<ApplicationDbContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddSingleton(configuration)
                .AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>))
                .AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
                .AddScoped<IDbQueryRunner, DbQueryRunner>()
                .AddScoped<Random>()
                .AddTransient<IEmailSender>(x => new SendGridEmailSender(configuration["SendGrid:ApiKey"]))
                .AddTransient<IEmailSender, NullMessageSender>()
                .AddTransient<IContestsService, ContestsService>()
                .AddTransient<IQuestionsService, QuestionsService>()
                .AddTransient<IAnswersService, AnswersService>()
                .AddTransient<IRoomsService, RoomsService>()
                .AddTransient<IApplicationSettingsService, ApplicationSettingsService>()
                .AddTransient<ICloudinaryService, CloudinaryService>()
                .AddTransient<IContactsService, ContactsService>();

        public static IServiceCollection AddApplicationConfigurations(this IServiceCollection services)
        {
            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services
                .AddRazorPages();

            services
                .AddResponseCompression();

            services
                .AddSignalR(options => { options.EnableDetailedErrors = true; });

            return services;
        }

        public static IServiceCollection ConfigureCookie(this IServiceCollection services)
            => services
                .Configure<CookiePolicyOptions>(
                    options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
            => services
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

        public static IServiceCollection AddExternalLogins(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication()
                .AddFacebook(options =>
                {
                    options.AppId = configuration["FacebookSettings:AppId"];
                    options.AppSecret = configuration["FacebookSettings:AppSecret"];
                })
                .AddGoogle(options =>
                {
                    options.ClientId = configuration["GoogleSettings:ClientId"];
                    options.ClientSecret = configuration["GoogleSettings:ClientSecret"];
                });

            return services;
        }

        public static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
        {
            var account = new Account(
                configuration["CloudinarySettings:CloudName"],
                configuration["CloudinarySettings:ApiKey"],
                configuration["CloudinarySettings:ApiSecret"]);

            var cloudinary = new Cloudinary(account);
            services.AddSingleton(cloudinary);

            return services;
        }
    }
}
