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
    [CustomInterface("Интерфейс для работы с письмами")]
    public interface IMessageInfoService
    {
        [CustomMethod("Метод получения списка писем")]
        List<InfoMessageViewModel> GetList();

        [CustomMethod("Метод получения письма по id")]
        InfoMessageViewModel GetElement(int id);

        [CustomMethod("Метод добавления письма")]
        void AddElement(InfoMessageBindingModel model);
    }
}
