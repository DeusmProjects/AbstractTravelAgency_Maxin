using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using System.Web.Mvc;

namespace AbstractTravelAgencyViewWeb.Controllers
{
    public class ConditionsController : Controller
    {
        private IConditionService service = Globals.ConditionService;
        // GET: Conditions
        public ActionResult Index()
        {
            return View(service.GetList());
        }

        // GET: Conditions/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost()
        {
            service.AddElement(new ConditionBindingModel
            {
                ConditionName = Request["ConditionName"]
            });
            return RedirectToAction("Index");
        }

        // GET: Conditions/Edit/5
        public ActionResult Edit(int id)
        {
            var viewModel = service.GetElement(id);
            var bindingModel = new ConditionBindingModel
            {
                ConditionId = id,
                ConditionName = viewModel.ConditionName
            };
            return View(bindingModel);
        }

        [HttpPost]
        public ActionResult EditPost()
        {
            service.UpdElement(new ConditionBindingModel
            {
                ConditionId = int.Parse(Request["ConditionId"]),
                ConditionName = Request["ConditionName"]
            });
            return RedirectToAction("Index");
        }

        // GET: Conditions/Delete/5
        public ActionResult Delete(int id)
        {
            service.DelElement(id);
            return RedirectToAction("Index");
        }
    }
}