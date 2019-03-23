using AbstractTravelAgencyModel;
using AbstractTravelAgencyServiceDAL.BindingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
