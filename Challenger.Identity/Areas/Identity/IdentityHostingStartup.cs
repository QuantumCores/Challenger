using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Challenger.Identity.Areas.Identity.IdentityHostingStartup))]
namespace Challenger.Identity.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}