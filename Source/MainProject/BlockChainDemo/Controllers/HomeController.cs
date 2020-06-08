using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockChainDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateWallet()
        {
            return View();
        }

        public ActionResult Account()
        {
            return View();
        }

        public ActionResult Transfer()
        {
            return View();
        }

        public ActionResult History()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Ví điện tử demo.";

            return View();
        }
    }
}