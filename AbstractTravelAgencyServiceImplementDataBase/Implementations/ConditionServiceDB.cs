using AbstractTravelAgencyModel;
using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceDAL.ViewModel;
using AbstractTravelAgencyServiceImplementDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceImplementDataBase.Implementations
{
    public class ConditionServiceDB : IConditionService
    {
        private AbstractDbScope context;

        public ConditionServiceDB(AbstractDbScope context)
        {
            this.context = context;
        }

        public List<ConditionViewModel> GetList()
        {
            List<ConditionViewModel> result = context.Conditions.Select(rec => new ConditionViewModel
            {
                Id = rec.Id,
                ConditionName = rec.ConditionName
            })
            .ToList();
            return result;
        }

        public ConditionViewModel GetElement(int id)
        {
            Condition element = context.Conditions.FirstOrDefault(rec => rec.Id == id);
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
            Condition element = context.Conditions.FirstOrDefault(rec => rec.ConditionName == model.ConditionName);
            if (element != null)
            {
                throw new Exception("Уже есть такое изделие");
            }
            context.Conditions.Add(new Condition
            {
                ConditionName = model.ConditionName
            });
            context.SaveChanges();
        }

        public void UpdElement(ConditionBindingModel model)
        {
            Condition element = context.Conditions.FirstOrDefault(rec => rec.ConditionName == model.ConditionName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть такое условие");
            }
            element = context.Conditions.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ConditionName = model.ConditionName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Condition element = context.Conditions.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Conditions.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
