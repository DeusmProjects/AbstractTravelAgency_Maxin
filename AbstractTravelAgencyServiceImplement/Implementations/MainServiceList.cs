using AbstractTravelAgencyModel;
using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceImplement.Implementations
{
    public class MainServiceList : IMainService
    {
        private DataListSingleton source;

        public MainServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<BookingViewModel> GetList()
        {
            List<BookingViewModel> result = new List<BookingViewModel>();
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                string clientFIO = string.Empty;
                for (int j = 0; j < source.Customers.Count; ++j)
                {
                    if (source.Customers[j].Id == source.Bookings[i].CustomerId)
                    {
                        clientFIO = source.Customers[j].CustomerFIO;
                        break;
                    }
                }
                string voucherName = string.Empty;
                for (int j = 0; j < source.Vouchers.Count; ++j)
                {
                    if (source.Vouchers[j].Id == source.Bookings[i].VoucherId)
                    {
                        voucherName = source.Vouchers[j].VoucherName;
                        break;
                    }
                }
                result.Add(new BookingViewModel
                {
                    Id = source.Bookings[i].Id,
                    CustomerId = source.Bookings[i].CustomerId,
                    CustomerFIO = clientFIO,
                    VoucherId = source.Bookings[i].VoucherId,
                    VoucherName = voucherName,
                    Amount = source.Bookings[i].Amount,
                    TotalSum = source.Bookings[i].TotalSum,
                    DateCreateBooking = source.Bookings[i].DataCreateBooking.ToLongDateString(),
                    DateImplementBooking = source.Bookings[i].DateImplementBooking?.ToLongDateString(),
                    StatusBooking = source.Bookings[i].StatusBooking.ToString()
                });
            }
            return result;
        }

        public void CreateBooking(BookingBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                if (source.Bookings[i].Id > maxId)
                {
                    maxId = source.Customers[i].Id;
                }
            }
            source.Bookings.Add(new Booking
            {
                Id = maxId + 1,
                CustomerId = model.CustomerId,
                VoucherId = model.VoucherId,
                DataCreateBooking = DateTime.Now,
                Amount = model.Amount,
                TotalSum = model.TotalSum,
                StatusBooking = BookingStatus.Принят
            });
        }
        public void TakeBookingInWork(BookingBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                if (source.Bookings[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Bookings[index].StatusBooking != BookingStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            source.Bookings[index].DateImplementBooking = DateTime.Now;
            source.Bookings[index].StatusBooking = BookingStatus.Выполняется;
        }

        public void FinishBooking(BookingBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                if (source.Customers[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Bookings[index].StatusBooking != BookingStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            source.Bookings[index].StatusBooking = BookingStatus.Готов;
        }

        public void PayBooking(BookingBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                if (source.Customers[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Bookings[index].StatusBooking != BookingStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            source.Bookings[index].StatusBooking = BookingStatus.Оплачен;
        }
    }
}
