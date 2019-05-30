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
    [CustomInterface("Интерфейс для работы с исполнителями")]
    public interface IExecutorService
    {
        [CustomMethod("Метод получения списка исполнителей")]
        List<ExecutorViewModel> GetList();

        [CustomMethod("Метод получения исполнителя по id")]
        ExecutorViewModel GetElement(int id);

        [CustomMethod("Метод добавления исполнителя")]
        void AddElement(ExecutorBindingModel model);

        [CustomMethod("Метод изменения данных по исполнителю")]
        void UpdElement(ExecutorBindingModel model);

        [CustomMethod("Метод удаления исполнителя")]
        void DelElement(int id);

        [CustomMethod("Метод получения свободного исполнителя")]
        ExecutorViewModel GetFreeWorker();
    }
}
