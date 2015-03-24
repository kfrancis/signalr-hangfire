using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleCore
{
    public class TaskRunner
    {
        public TaskRunner()
        {
        }

        public void PerformTask(string hostUrl, int taskId)
        {
            Console.WriteLine(taskId);

            NotifyTaskComplete(hostUrl, taskId);
        }
 
        private void NotifyTaskComplete(string hostUrl, int taskId)
        {
            try
            {
                var hubConnection = new HubConnection(hostUrl);
                var hub = hubConnection.CreateHubProxy("NotificationHub");

                hubConnection.Start().Wait();

                hub.Invoke("SendMessage", taskId.ToString()).Wait();
            }
            catch (Exception ex)
            {
            }
        }

        public void PerformLongTask(string hostUrl, int taskId)
        {
            Thread.Sleep(TimeSpan.FromSeconds(30));
            Console.WriteLine(taskId);

            NotifyTaskComplete(hostUrl, taskId);
        }

        /// <summary>
        /// Enqueues the short running task action
        /// </summary>
        /// <param name="taskId"></param>
        public static void Queue(string hostUrl, int taskId)
        {
            JobStorage.Current = new SqlServerStorage("HangfireStorage");
            BackgroundJob.Enqueue<TaskRunner>(x => x.PerformTask(hostUrl, taskId));
        }

        /// <summary>
        /// Enqueues the long running task action
        /// </summary>
        /// <param name="taskId"></param>
        public static void QueueLong(string hostUrl, int taskId)
        {
            JobStorage.Current = new SqlServerStorage("HangfireStorage");
            BackgroundJob.Enqueue<TaskRunner>(x => x.PerformLongTask(hostUrl, taskId));
        }
    }
}
