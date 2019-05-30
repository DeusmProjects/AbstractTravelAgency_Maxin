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
    [CustomInterface("Интерфейс для работы с городами")]
    public interface ICityService
    {
        [CustomMethod("Метод получения списка городов")]
        List<CityViewModel> GetList();

        [CustomMethod("Метод получения города по id")]
        CityViewModel GetElement(int id);

        [CustomMethod("Метод добавления города")]
        void AddElement(CityBindingModel model);

        [CustomMethod("Метод изменения данных по городу")]
        void UpdElement(CityBindingModel model);

        [CustomMethod("Метод удаления города")]
        void DelElement(int id);
    }
}
