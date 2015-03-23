using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire;
using Hangfire.SqlServer;

[assembly: OwinStartup(typeof(SampleService.Startup))]

namespace SampleService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage();
            app.UseWelcomePage("/");

            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.UseHangfire(config =>
            {
                config.UseSqlServerStorage("DefaultConnection");
                config.UseServer();
            });
        }
    }
}
