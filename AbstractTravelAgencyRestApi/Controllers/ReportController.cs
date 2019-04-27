using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbstractTravelAgencyRestApi.Controllers
{
    public class ReportController : ApiController
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetCitiesLoad()
        {
            var list = _service.GetCitiesLoad();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public IHttpActionResult GetCustomerBookings(ReportBindingModel model)
        {
            var list = _service.GetCustomerBookings(model);
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void SaveVoucherCost(ReportBindingModel model)
        {
            _service.SaveVoucherCost(model);
        }

        [HttpPost]
        public void SaveCitiesLoad(ReportBindingModel model)
        {
            _service.SaveCitiesLoad(model);
        }

        [HttpPost]
        public void SaveCustomerBookings(ReportBindingModel model)
        {
            _service.SaveCustomerBookings(model);
        }
    }
}
