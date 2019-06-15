using AbstractTravelAgencyServiceImplementDataBase;
using System.Linq;
using System.Web.Mvc;

namespace AbstractTravelAgencyViewWeb.Controllers
{
    public class ReportController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            AbstractDbScope entities = new AbstractDbScope();
            return View(from customer in entities.Bookings select customer);
        }
    }
}