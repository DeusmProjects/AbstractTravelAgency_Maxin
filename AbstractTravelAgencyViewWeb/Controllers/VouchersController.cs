using AbstractTravelAgencyServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbstractTravelAgencyViewWeb.Controllers
{
    public class VouchersController : Controller
    {
        public IVoucherService service = Globals.VoucherService;
        // GET: Pizzas
        public ActionResult Index()
        {
            return View(service.GetList());
        }

        public ActionResult Delete(int id)
        {
            service.DelElement(id);
            return RedirectToAction("Index");
        }
    }
}