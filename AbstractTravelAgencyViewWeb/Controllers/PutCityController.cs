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

            var conditions = new SelectList(conditionService.GetList(), "Id", "ConditionName");
            ViewBag.Conditions = conditions;

            var cities = new SelectList(cityService.GetList(), "Id", "CityName");
            ViewBag.Cities = cities;
            return View((CityViewModel)Session["Cities"]);
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

            return RedirectToAction("List", "Cities");
        }
    }
}