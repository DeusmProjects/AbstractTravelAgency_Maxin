using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceDAL.Interfaces
{
    public interface IMessageInfoService
    {
        List<InfoMessageViewModel> GetList();
        InfoMessageViewModel GetElement(int id);
        void AddElement(InfoMessageBindingModel model);
    }
}
