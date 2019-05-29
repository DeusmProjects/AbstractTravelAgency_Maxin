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
    [CustomInterface("Интерфейс для работы с отчетами")]
    public interface IReportService
    {
        [CustomMethod("Метод получения прайса путевок")]
        void SaveVoucherCost(ReportBindingModel model);

        [CustomMethod("Метод получения загруженности городов")]
        List<CitiesLoadViewModel> GetCitiesLoad();

        [CustomMethod("Метод сохранения загруженности городов")]
        void SaveCitiesLoad(ReportBindingModel model);

        [CustomMethod("Метод получения заказов клиентов")]
        List<CustomerBookingsModel> GetCustomerBookings(ReportBindingModel model);

        [CustomMethod("Метод сохранения заказов клиентов")]
        void SaveCustomerBookings(ReportBindingModel model);
    }
}
