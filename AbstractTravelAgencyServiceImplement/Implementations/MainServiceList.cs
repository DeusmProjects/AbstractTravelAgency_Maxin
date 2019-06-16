using AbstractTravelAgencyModel;
using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

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
            List<BookingViewModel> result = source.Bookings
             .Select(rec => new BookingViewModel
             {
                 Id = rec.Id,
                 CustomerId = rec.CustomerId,
                 VoucherId = rec.VoucherId,
                 DateCreateBooking = rec.DateCreateBooking.ToLongDateString(),
                 DateImplementBooking = rec.DateImplementBooking?.ToLongDateString(),
                 StatusBooking = rec.StatusBooking.ToString(),
                 Amount = rec.Amount,
                 TotalSum = rec.TotalSum,
                 CustomerFIO = source.Customers.FirstOrDefault(recC => recC.Id == rec.CustomerId)?.CustomerFIO,
                 VoucherName = source.Vouchers.FirstOrDefault(recP => recP.Id == rec.VoucherId)?.VoucherName,
             })
             .ToList();
            return result;
        }

        public void CreateBooking(BookingBindingModel model)
        {
            int maxId = source.Bookings.Count > 0 ? source.Bookings.Max(rec => rec.Id) : 0;
            source.Bookings.Add(new Booking
            {
                Id = maxId + 1,
                CustomerId = model.CustomerId,
                VoucherId = model.VoucherId,
                DateCreateBooking = DateTime.Now,
                Amount = model.Amount,
                TotalSum = model.TotalSum,
                StatusBooking = BookingStatus.Принят
            });
        }

        public void TakeBookingInWork(BookingBindingModel model)
        {
            Booking element = source.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.StatusBooking != BookingStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            var voucherConditions = source.VoucherConditions.Where(rec => rec.VoucherId == element.VoucherId);

            foreach (var voucherCondition in voucherConditions)
            {
                int countOnCities = source.CityConditions.Where(rec => rec.ConditionId == voucherCondition.ConditionId)
               .Sum(rec => rec.Amount);
                if (countOnCities < voucherCondition.Amount * element.Amount)
                {
                    var conditionName = source.Conditions.FirstOrDefault(rec => rec.Id == voucherCondition.ConditionId);
                    throw new Exception("Не достаточно условий " +
                   conditionName?.ConditionName + " требуется " + (voucherCondition.Amount * element.Amount) +
                   ", в наличии " + countOnCities);
                }
            }
            // списываем
            foreach (var voucherCondition in voucherConditions)
            {
                int countOnCities = voucherCondition.Amount * element.Amount;
                var cityConditions = source.CityConditions.Where(rec => rec.ConditionId == voucherCondition.ConditionId);
                foreach (var cityCondition in cityConditions)
                {
                    if (cityCondition.Amount >= countOnCities)
                    {
                        cityCondition.Amount -= countOnCities;
                        break;
                    }
                    else
                    {
                        countOnCities -= cityCondition.Amount;
                        cityCondition.Amount = 0;
                    }
                }
            }

            element.DateImplementBooking = DateTime.Now;
            element.StatusBooking = BookingStatus.Выполняется;
        }

        public void PayBooking(BookingBindingModel model)
        {
            Booking element = source.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.StatusBooking != BookingStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.StatusBooking = BookingStatus.Оплачен;
        }

        public void FinishBooking(BookingBindingModel model)
        {
            Booking element = source.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.StatusBooking != BookingStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.StatusBooking = BookingStatus.Готов;
        }

        public void PutConditionOnCity(CityConditionBindingModel model)
        {
            CityCondition element = source.CityConditions.FirstOrDefault(rec =>
           rec.CityId == model.CityId && rec.ConditionId == model.ConditionId);
            if (element != null)
            {
                element.Amount += model.Amount;
            }
            else
            {
                int maxId = source.CityConditions.Count > 0 ? source.CityConditions.Max(rec => rec.Id) : 0;
                source.CityConditions.Add(new CityCondition
                {
                    Id = ++maxId,
                    CityId = model.CityId,
                    ConditionId = model.ConditionId,
                    Amount = model.Amount
                });
            }
        }
    }
}
