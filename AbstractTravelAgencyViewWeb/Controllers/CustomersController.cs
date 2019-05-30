using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using System.Web.Mvc;

namespace AbstractTravelAgencyViewWeb.Controllers
{
    public class CustomersController : Controller
    {
        public ICustomerService service = Globals.CustomerService;
        // GET: Customers
        public ActionResult Index()
        {
            return View(service.GetList());
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreatePost()
        {
            service.AddElement(new CustomerBindingModel
            {
                CustomerFIO = Request["CustomerFIO"]
            });
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var viewModel = service.GetElement(id);
            var bindingModel = new CustomerBindingModel
            {
                CustomerId = id,
                CustomerFIO = viewModel.CustomerFIO
            };
            return View(bindingModel);
        }

        [HttpPost]
        public ActionResult EditPost()
        {
            service.UpdElement(new CustomerBindingModel
            {
                CustomerId = int.Parse(Request["CustomerId"]),
                CustomerFIO = Request["CustomerFIO"]
            });
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            service.DelElement(id);
            return RedirectToAction("Index");
        }
    }
}