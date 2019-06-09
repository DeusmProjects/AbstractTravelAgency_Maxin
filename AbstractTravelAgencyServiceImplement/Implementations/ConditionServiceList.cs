using AbstractTravelAgencyModel;
using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

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
            List<ConditionViewModel> result = source.Conditions.Select(rec => new ConditionViewModel
            {
                Id = rec.Id,
                ConditionName = rec.ConditionName
            })
            .ToList();
            return result;
        }

        public ConditionViewModel GetElement(int id)
        {
            Condition element = source.Conditions.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ConditionViewModel
                {
                    Id = element.Id,
                    ConditionName = element.ConditionName
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ConditionBindingModel model)
        {
            Condition element = source.Conditions.FirstOrDefault(rec => rec.ConditionName 
            == model.ConditionName);
            if (element != null)
            {
            throw new Exception("Уже есть такое условие");
        }
            int maxId = source.Conditions.Count > 0 ? source.Conditions.Max(rec => rec.Id) : 0;
            source.Conditions.Add(new Condition
            {
                Id = maxId + 1,
                ConditionName = model.ConditionName
            });
        }

        public void UpdElement(ConditionBindingModel model)
        {
            Condition element = source.Conditions.FirstOrDefault(rec => rec.ConditionName == model.ConditionName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть условие с таким названием");
            }

            element = source.Conditions.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ConditionName = model.ConditionName;
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
