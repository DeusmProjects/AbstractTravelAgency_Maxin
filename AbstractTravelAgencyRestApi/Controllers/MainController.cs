using AbstractTravelAgencyRestApi.Services;
using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbstractTravelAgencyRestApi.Controllers
{
    public class MainController : ApiController
    {
        private readonly IMainService _service;
        private readonly IExecutorService _serviceExecutor;

        public MainController(IMainService service, IExecutorService serviceExecutor)
        {
            _service = service;
            _serviceExecutor = serviceExecutor;
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

        [HttpPost]
        public void CreateBooking(BookingBindingModel model)
        {
            _service.CreateBooking(model);
        }
        [HttpPost]

        public void PayBooking(BookingBindingModel model)
        {
            _service.PayBooking(model);
        }

        [HttpPost]
        public void PutConditionOnCity(CityConditionBindingModel model)
        {
            _service.PutConditionOnCity(model);
        }

        [HttpPost]
        public void StartWork()
        {
            List<BookingViewModel> bookingss = _service.GetFreeBookings();
            foreach (var bookings in bookingss)
            {
                ExecutorViewModel impl = _serviceExecutor.GetFreeWorker();
                if (impl == null)
                {
                    throw new Exception("Нет сотрудников");
                }
                new WorkExecutor(_service, _serviceExecutor, impl.Id, bookings.Id);
            }
        }

        [HttpGet]
        public IHttpActionResult GetInfo()
        {
            ReflectionService service = new ReflectionService();
            var list = service.GetInfoByAssembly();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
    }
}
