using System;
using System.Collections.Generic;
using System.Configuration.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace SampleService
{
    /// <summary>
    /// Class used when debugging
    /// </summary>
    public class StandAlone
    {
        /// <summary>
        /// Instance of the OWIN server
        /// </summary>
        private IDisposable _appServer;

        /// <summary>
        /// Constructor
        /// </summary>
        public StandAlone()
        {
        }

        internal void Start()
        {
            StartOptions startOptions = new StartOptions();
            string webAppPort = ConfigurationManager.Instance.AppSettings.AppSetting<string>("Hangfire.WebAppPort", () => "9095");
            startOptions.Urls.Add(string.Format("http://localhost:{0}", webAppPort));
            startOptions.Urls.Add(string.Format("http://127.0.0.1:{0}", webAppPort));
            startOptions.Urls.Add(string.Format("http://{0}:{1}", Environment.MachineName, webAppPort));

            _appServer = WebApp.Start<Startup>(startOptions);
        }

        internal void Stop()
        {
            if (_appServer != null) { _appServer.Dispose(); }
        }
    }
}
