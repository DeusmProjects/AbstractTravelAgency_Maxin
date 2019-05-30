using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbstractTravelAgencyViewWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Customers()
        {
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Conditions()
        {
            return RedirectToAction("Index", "Conditions");
        }

        public ActionResult Vouchers()
        {
            return RedirectToAction("Index", "Vouchers");
        }

        public ActionResult Main()
        {
            return RedirectToAction("Index", "Main");
        }
    }
}