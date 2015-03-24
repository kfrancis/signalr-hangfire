using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(SampleWeb.Startup))]
namespace SampleWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // Important: Before SignalR is configured (and the route /signalr is added), we need to load
            // the external assembly which contains the hub "NotificationHub" that we'll ultimately use
            // to do all the talking from client to client. This solution comes from:
            // https://stackoverflow.com/questions/16779057/map-hubs-in-referenced-project
            AppDomain.CurrentDomain.Load(typeof(SampleCore.NotificationHub).Assembly.FullName);

            app.MapSignalR();
        }
    }
}
