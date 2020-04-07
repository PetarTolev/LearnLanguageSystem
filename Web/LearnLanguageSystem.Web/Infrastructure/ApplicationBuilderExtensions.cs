namespace LearnLanguageSystem.Web.Infrastructure
{
    using LearnLanguageSystem.Data;
    using LearnLanguageSystem.Data.Seeding;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (env.IsDevelopment())
            {
                dbContext.Database.Migrate();
            }

            new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter()
                .GetResult();
        }
    }
}
