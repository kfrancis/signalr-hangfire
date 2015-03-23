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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult ShortTask(int taskId)
        {
            TaskRunner.Queue(taskId);

            return Json(true, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult LongTask(int taskId)
        {
            TaskRunner.QueueLong(taskId);

            return Json(true, JsonRequestBehavior.DenyGet);
        }
    }
}