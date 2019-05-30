using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AbstractTravelAgencyViewWeb.Controllers
{
    public class PutCityController : Controller
    {
        private IConditionService conditionService = Globals.ConditionService;
        private ICityService cityService = Globals.CityService;
        private IMainService mainService = Globals.MainService;

        public ActionResult Index()
        {
            if (Session["Cities"] == null)
            {
                var city = new CityViewModel();
                city.CityConditions = new List<CityConditionViewModel>();
                Session["Cities"] = city;
            }
            

            var conditions = new SelectList(conditionService.GetList(), "ConditionId", "ConditionName");
            ViewBag.Conditions = conditions;

            var cities = new SelectList(cityService.GetList(), "CityId", "CityName");
            ViewBag.Cities = cities;
            return View((VoucherViewModel)Session["Cities"]);
        }

        [HttpPost]
        public ActionResult PutCityPost()
        {
            mainService.PutConditionOnCity(new CityConditionBindingModel
            {
                ConditionId = int.Parse(Request["ConditionId"]),
                CityId = int.Parse(Request["CityId"]),
                Amount = int.Parse(Request["Amount"])
            });

            var city = (CityViewModel)Session["Cities"];
            var cityConditions = new CityConditionViewModel
            {
                ConditionId = int.Parse(Request["ConditionId"]),
                CityId = int.Parse(Request["CityId"]),
                ConditionName = conditionService.GetElement(int.Parse(Request["ConditionId"])).ConditionName,
                Amount = int.Parse(Request["Amount"])
            };
            city.CityConditions.Add(cityConditions);
            Session["Cities"] = city;

            return RedirectToAction("List", "Cities");
        }
    }
}