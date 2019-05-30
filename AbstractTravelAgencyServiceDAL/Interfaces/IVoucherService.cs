using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractTravelAgencyServiceDAL.Interfaces
{
    public interface IVoucherService
    {
        List<VoucherViewModel> GetList();

        VoucherViewModel GetElement(int id);

        void AddElement(VoucherBindingModel model);

        void UpdElement(VoucherBindingModel model);

        void DelElement(int id);
    }
}
