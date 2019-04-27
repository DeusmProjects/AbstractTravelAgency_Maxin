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
    public class ExecutorController : ApiController
    {
        private readonly IExecutorService _service;
        public ExecutorController(IExecutorService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var element = _service.GetElement(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }

        [HttpPost]
        public void AddElement(ExecutorBindingModel model)
        {
            _service.AddElement(model);
        }

        [HttpPost]
        public void UpdElement(ExecutorBindingModel model)
        {
            _service.UpdElement(model);
        }

        [HttpPost]
        public void DelElement(ExecutorBindingModel model)
        {
            _service.DelElement(model.Id);
        }

    }
}
