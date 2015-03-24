using SampleCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleWeb.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Default view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// ShortTask is a method called from javascript (the "Short" button) which will
        /// cause a short task to be enqueued in hangfire
        /// </summary>
        /// <param name="taskId">The id of the task</param>
        /// <returns>Returns true</returns>
        [HttpPost]
        public ActionResult ShortTask(int taskId)
        {
            TaskRunner.Queue(GetHostAddress(), taskId);

            return Json(true, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// LongTask is a method called from javascript (the "Long" button) which will
        /// cause a long task to be enqueued in hangfire
        /// </summary>
        /// <param name="taskId">The id of the task</param>
        /// <returns>Returns true</returns>
        [HttpPost]
        public ActionResult LongTask(int taskId)
        {
            TaskRunner.QueueLong(GetHostAddress(), taskId);

            return Json(true, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// In order for the job to be able to create the HubConnection to the SignalR host,
        /// we need to include the host address as a parameter for the queued job.
        /// </summary>
        /// <returns>The host address</returns>
        private string GetHostAddress()
        {
            var request = this.HttpContext.Request;
            return string.Format("{0}://{1}", request.Url.Scheme, request.Url.Authority);
        }
    }
}