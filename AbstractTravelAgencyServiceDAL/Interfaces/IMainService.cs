using AbstractTravelAgencyServiceDAL.Attributies;
using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с бронями")]
    public interface IMainService
    {
        [CustomMethod("Метод получения списка броней")]
        List<BookingViewModel> GetList();

        [CustomMethod("Метод получения списка свободных броней")]
        List<BookingViewModel> GetFreeBookings();

        [CustomMethod("Метод создания брони")]
        void CreateBooking(BookingBindingModel model);

        [CustomMethod("Метод отправки брони в работу")]
        void TakeBookingInWork(BookingBindingModel model);

        [CustomMethod("Метод выполнения брони")]
        void FinishBooking(BookingBindingModel model);

        [CustomMethod("Метод оплаты брони")]
        void PayBooking(BookingBindingModel model);

        [CustomMethod("Метод добавления условий в город")]
        void PutConditionOnCity(CityConditionBindingModel model);
    }
}
