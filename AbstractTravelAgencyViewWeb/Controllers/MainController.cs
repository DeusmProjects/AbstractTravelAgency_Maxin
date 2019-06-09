using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Web.Mvc;

namespace AbstractTravelAgencyViewWeb.Controllers
{
    public class MainController : Controller
    {
        private IVoucherService voucherService = Globals.VoucherService;
        private IMainService mainService = Globals.MainService;
        private ICustomerService customerService = Globals.CustomerService;


        // GET: VoucherOrder
        public ActionResult Index()
        {
            return View(mainService.GetList());
        }

        public ActionResult Create()
        {
            var vouchers = new SelectList(voucherService.GetList(), "Id", "VoucherName");
            var customers = new SelectList(customerService.GetList(), "Id", "CustomerFIO");
            ViewBag.Vouchers = vouchers;
            ViewBag.Customers = customers;
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost()
        {
            var customerId = int.Parse(Request["CustomerId"]);
            var voucherId = int.Parse(Request["VoucherId"]);
            var amount = int.Parse(Request["Amount"]);
            var totalSum = CalcSum(voucherId, amount);

            mainService.CreateBooking(new BookingBindingModel
            {
                CustomerId = customerId,
                VoucherId = voucherId,
                Amount = amount,
                TotalSum = totalSum

            });
            return RedirectToAction("Index");
        }

        private Decimal CalcSum(int voucherId, int amount)
        {
            VoucherViewModel voucher = voucherService.GetElement(voucherId);
            return amount * voucher.Cost;
        }

        public ActionResult SetStatus(int id, string status)
        {
            try
            {
                switch (status)
                {
                    case "Processing":
                        mainService.TakeBookingInWork(new BookingBindingModel { Id = id });
                        break;
                    case "Ready":
                        mainService.FinishBooking(new BookingBindingModel { Id = id });
                        break;
                    case "Paid":
                        mainService.PayBooking(new BookingBindingModel { Id = id });
                        break;
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }


            return RedirectToAction("Index");
        }
    }
}