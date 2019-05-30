using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System.Collections.Generic;

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
