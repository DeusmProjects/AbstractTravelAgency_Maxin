using AbstractTravelAgencyModel;
using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceImplement.Implementations
{
    public class ConditionServiceList : IConditionService
    {
        private DataListSingleton source;
        public ConditionServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<ConditionViewModel> GetList()
        {
            List<ConditionViewModel> result = new List<ConditionViewModel>();
            for (int i = 0; i < source.Conditions.Count; ++i)
            {
                result.Add(new ConditionViewModel
                {
                    Id = source.Conditions[i].Id,
                    ConditionName = source.Conditions[i].ConditionName
                });
            }
            return result;
        }
        public ConditionViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Conditions.Count; ++i)
            {
                if (source.Conditions[i].Id == id)
                {
                    return new ConditionViewModel
                    {
                        Id = source.Conditions[i].Id,
                        ConditionName = source.Conditions[i].ConditionName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(ConditionBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Conditions.Count; ++i)
            {
                if (source.Conditions[i].Id > maxId)
                {
                    maxId = source.Conditions[i].Id;
                }
                if (source.Conditions[i].ConditionName == model.ConditionName)
                {
                    throw new Exception("Уже есть такое условие");
                }
            }
            source.Conditions.Add(new Condition
            {
                Id = maxId + 1,
                ConditionName = model.ConditionName
            });
        }
        public void UpdElement(ConditionBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Conditions.Count; ++i)
            {
                if (source.Conditions[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Conditions[i].ConditionName == model.ConditionName && source.Conditions[i].Id != model.Id)
                {
                    throw new Exception("Уже есть такое условие");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Conditions[index].ConditionName = model.ConditionName;
        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.Conditions.Count; ++i)
            {
                if (source.Conditions[i].Id == id)
                {
                    source.Conditions.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
