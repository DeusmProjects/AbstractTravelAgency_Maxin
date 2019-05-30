using AbstractTravelAgencyModel;
using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;

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
                    ConditionId = source.Conditions[i].ConditionId,
                    ConditionName = source.Conditions[i].ConditionName
                });
            }
            return result;
        }

        public ConditionViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Conditions.Count; ++i)
            {
                if (source.Conditions[i].ConditionId == id)
                {
                    return new ConditionViewModel
                    {
                        ConditionId = source.Conditions[i].ConditionId,
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
                if (source.Conditions[i].ConditionId > maxId)
                {
                    maxId = source.Conditions[i].ConditionId;
                }
                if (source.Conditions[i].ConditionName == model.ConditionName)
                {
                    throw new Exception("Уже есть такое условие");
                }
            }
            source.Conditions.Add(new Condition
            {
                ConditionId = maxId + 1,
                ConditionName = model.ConditionName
            });
        }

        public void UpdElement(ConditionBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Conditions.Count; ++i)
            {
                if (source.Conditions[i].ConditionId == model.ConditionId)
                {
                    index = i;
                }
                if (source.Conditions[i].ConditionName == model.ConditionName && source.Conditions[i].ConditionId != model.ConditionId)
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
                if (source.Conditions[i].ConditionId == id)
                {
                    source.Conditions.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
