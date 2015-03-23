using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            var service = new StandAlone();
            service.Start();
            MessageBox.Show("Press 'OK' to stop the service", "Service Debug Mode", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            service.Stop();
            MessageBox.Show("Service Stopped", "Service Debug Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new Service1() 
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
