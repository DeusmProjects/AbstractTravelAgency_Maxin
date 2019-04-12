using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceDAL.Interfaces
{
    public interface IReportService
    {
        void SaveVoucherCost(ReportBindingModel model);
        List<CitiesLoadViewModel> GetCitiesLoad();
        void SaveCitiesLoad(ReportBindingModel model);
        List<CustomerBookingsModel> GetCustomerBookings(ReportBindingModel model);
        void SaveCustomerBookings(ReportBindingModel model);
    }
}
