using System;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyViewWeb;
using System.Web.Mvc;
using AbstractTravelAgencyServiceDAL.BindingModel;

namespace AbstractGarmentFactoryMVC.Controllers
{
    public class ReportController : Controller
    {
        IReportService reportService = Globals.ReportService;

        public ActionResult Index()
        {
            ViewBag.ServiceReport = reportService;
            return View();
        }

        public ActionResult CustomerBookings()
        {
            return View();
        }

        public ActionResult VoucherPrice()
        {
            return View();
        }

        public ActionResult CitiesLoad()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveCustomerBookings()
        {
            reportService.SaveCustomerBookings(new ReportBindingModel
            {
                DateFrom = new DateTime(2019, 5, 20),
                DateTo = DateTime.Now,
                FileName = "D:\\bookings.pdf"
            });

            return RedirectToAction("Index");
        }

        public ActionResult SaveCitiesLoad()
        {
            reportService.SaveCitiesLoad(new ReportBindingModel
            {
                DateFrom = new DateTime(2019, 5, 20),
                DateTo = DateTime.Now,
                FileName = "D:\\cities.xls"
            });

            return RedirectToAction("Index");
        }

        public ActionResult SaveVoucherPrice()
        {
            reportService.SaveVoucherCost(new ReportBindingModel
            {
                DateFrom = new DateTime(2019, 5, 20),
                DateTo = DateTime.Now,
                FileName = "D:\\price.docx"
            });

            return RedirectToAction("Index");
        }
    }
}