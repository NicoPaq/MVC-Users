using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /** Attention de bien penser à externaliser les scripts sur un cdn Jquery, JS,... **/
        /** Action Result pourra renvoyer un 
         * FileResult
         * JsonResult
         *** **/
    }
}