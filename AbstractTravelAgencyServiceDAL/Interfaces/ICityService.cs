using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceDAL.Interfaces
{
    public interface ICityService
    {
        List<CityViewModel> GetList();
        CityViewModel GetElement(int id);
        void AddElement(CityBindingModel model);
        void UpdElement(CityBindingModel model);
        void DelElement(int id);
    }
}
