using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(LearnLanguageSystem.Web.Areas.Identity.IdentityHostingStartup))]

namespace LearnLanguageSystem.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}
