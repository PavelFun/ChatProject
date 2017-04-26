using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatProject.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        /// <summary>
        /// Представление на экране браузера
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

    }
}
