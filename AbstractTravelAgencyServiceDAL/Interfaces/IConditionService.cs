using AbstractTravelAgencyModel;
using AbstractTravelAgencyServiceDAL.Attributies;
using AbstractTravelAgencyServiceDAL.BindingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с условиями")]
    public interface IConditionService
    {
        [CustomMethod("Метод получения списка условий")]
        List<ConditionViewModel> GetList();

        [CustomMethod("Метод получения условия по id")]
        ConditionViewModel GetElement(int id);

        [CustomMethod("Метод получения добавления условия")]
        void AddElement(ConditionBindingModel model);

        [CustomMethod("Метод изменения данных по условию")]
        void UpdElement(ConditionBindingModel model);

        [CustomMethod("Метод удаления условия")]
        void DelElement(int id);
    }
}
