using AbstractTravelAgencyModel;
using AbstractTravelAgencyServiceDAL.BindingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceDAL.Interfaces
{
    public interface IConditionService
    {
        List<ConditionViewModel> GetList();
        ConditionViewModel GetElement(int id);
        void AddElement(ConditionBindingModel model);
        void UpdElement(ConditionBindingModel model);
        void DelElement(int id);
    }
}
