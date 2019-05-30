using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AbstractTravelAgencyViewWeb.Controllers
{
    public class VoucherController : Controller
    {
        private IVoucherService service = Globals.VoucherService;
        private IConditionService conditionService = Globals.ConditionService;

        // GET: Vouchers
        public ActionResult Index()
        {
            if (Session["Voucher"] == null)
            {
                var voucher = new VoucherViewModel();
                voucher.VoucherConditions = new List<VoucherConditionViewModel>();
                Session["Voucher"] = voucher;
            }
            return View((VoucherViewModel)Session["Voucher"]);
        }

        public ActionResult AddCondition()
        {
            var conditions = new SelectList(conditionService.GetList(), "ConditionId", "ConditionName");
            ViewBag.Conditions = conditions;
            return View();
        }

        [HttpPost]
        public ActionResult AddConditionPost()
        {
            var voucher = (VoucherViewModel)Session["Voucher"];
            var ingredient = new VoucherConditionViewModel
            {
                ConditionId = int.Parse(Request["ConditionId"]),
                ConditionName = conditionService.GetElement(int.Parse(Request["ConditionId"])).ConditionName,
                Amount = int.Parse(Request["Amount"])
            };
            voucher.VoucherConditions.Add(ingredient);
            Session["Voucher"] = voucher;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreateVoucherPost()
        {
            var voucher = (VoucherViewModel)Session["Voucher"];
            var voucherConditions = new List<VoucherConditionBindingModel>();
            for (int i = 0; i < voucher.VoucherConditions.Count; ++i)
            {
                voucherConditions.Add(new VoucherConditionBindingModel
                {
                    VoucherConditionId = voucher.VoucherConditions[i].VoucherConditionId,
                    VoucherId = voucher.VoucherConditions[i].VoucherId,
                    ConditionId = voucher.VoucherConditions[i].ConditionId,
                    Amount = voucher.VoucherConditions[i].Amount
                });
            }
            service.AddElement(new VoucherBindingModel
            {
                VoucherName = Request["VoucherName"],
                Cost = Convert.ToDecimal(Request["Cost"]),
                VoucherConditions = voucherConditions
            });
            Session.Remove("Voucher");
            return RedirectToAction("Index", "Vouchers");
        }
    }
}