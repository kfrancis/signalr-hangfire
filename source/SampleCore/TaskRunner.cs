using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNet.SignalR;
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

        public void PerformTask(int taskId)
        {
            Console.WriteLine(taskId);

            NotifyTaskComplete(taskId);
        }
 
        private void NotifyTaskComplete(int taskId)
        {
            try
            {
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                if (hubContext != null)
                {
                    hubContext.Clients.All.sendMessage(string.Format("Task {0} completed.", taskId));
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void PerformLongTask(int taskId)
        {
            Thread.Sleep(TimeSpan.FromSeconds(30));
            Console.WriteLine(taskId);
            NotifyTaskComplete(taskId);
        }

        /// <summary>
        /// Enqueues the short running task action
        /// </summary>
        /// <param name="taskId"></param>
        public static void Queue(int taskId)
        {
            JobStorage.Current = new SqlServerStorage("HangfireStorage");
            BackgroundJob.Enqueue<TaskRunner>(x => x.PerformTask(taskId));
        }

        /// <summary>
        /// Enqueues the long running task action
        /// </summary>
        /// <param name="taskId"></param>
        public static void QueueLong(int taskId)
        {
            JobStorage.Current = new SqlServerStorage("HangfireStorage");
            BackgroundJob.Enqueue<TaskRunner>(x => x.PerformLongTask(taskId));
        }
    }
}
