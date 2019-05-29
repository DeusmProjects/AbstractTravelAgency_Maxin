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
    [CustomInterface("Интерфейс для работы с путевками")]
    public interface IVoucherService
    {
        [CustomMethod("Метод получения списка путевок")]
        List<VoucherViewModel> GetList();

        [CustomMethod("Метод получения путевки по id")]
        VoucherViewModel GetElement(int id);

        [CustomMethod("Метод добавления путевки")]
        void AddElement(VoucherBindingModel model);

        [CustomMethod("Метод изменения данных по путевке")]
        void UpdElement(VoucherBindingModel model);

        [CustomMethod("Метод удаления путевки")]
        void DelElement(int id);
    }
}
