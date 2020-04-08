namespace LearnLanguageSystem.Web
{
    using System.Reflection;

    using LearnLanguageSystem.Services.Mapping;
    using LearnLanguageSystem.Web.Infrastructure;
    using LearnLanguageSystem.Web.ViewModels;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration) => this.configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        => services
                .AddDatabase(this.configuration)
                .AddApplicationConfigurations()
                .ConfigureCookie()
                .AddIdentity()
                .ConfigureIdentity()
                .AddExternalLogins(this.configuration)
                .AddSingleton(this.configuration)
                .AddApplicationServices();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            if (env.IsDevelopment())
            {
                app
                    .UseDeveloperExceptionPage()
                    .UseDatabaseErrorPage();
            }
            else
            {
                app
                    .UseExceptionHandler("/Home/Error")
                    .UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseCookiePolicy()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(
                    endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    })
                .ApplyMigrations(env);
        }
    }
}
