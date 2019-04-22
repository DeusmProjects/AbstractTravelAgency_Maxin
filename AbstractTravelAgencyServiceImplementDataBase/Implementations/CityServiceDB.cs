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

namespace AbstractGarmentFactoryServiceImplementDataBase.Implementations
{
    public class CityServiceDB : ICityService
    {
        private AbstractDbScope context;

        public CityServiceDB(AbstractDbScope context)
        {
            this.context = context;
        }

        public List<CityViewModel> GetList()
        {
            List<CityViewModel> result = context.Cities.Select(rec => new
            CityViewModel
            {
                Id = rec.Id,
                CityName = rec.CityName,
                CityConditions = context.CityConditions
            .Where(recPC => recPC.CityId == rec.Id)
            .Select(recPC => new CityConditionViewModel
            {
                Id = recPC.Id,
                CityId = recPC.CityId,
                ConditionId = recPC.ConditionId,
                ConditionName = recPC.Condition.ConditionName,
                Amount = recPC.Amount
            })
            .ToList()
            })
            .ToList();
            return result;
        }

        public CityViewModel GetElement(int id)
        {
            City element = context.Cities.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CityViewModel
                {
                    Id = element.Id,
                    CityName = element.CityName,
                    CityConditions = context.CityConditions
                    .Where(recPC => recPC.CityId == element.Id)
                    .Select(recPC => new CityConditionViewModel
                    {
                        Id = recPC.Id,
                        CityId = recPC.CityId,
                        ConditionId = recPC.ConditionId,
                        ConditionName = recPC.Condition.ConditionName,
                        Amount = recPC.Amount
                    })
                    .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CityBindingModel model)
        {
            City element = context.Cities.FirstOrDefault(rec => rec.CityName == model.CityName);
            if (element != null)
            {
                throw new Exception("Уже есть такой склад");
            }
            context.Cities.Add(new City
            {
                CityName = model.CityName
            });
            context.SaveChanges();
        }

        public void UpdElement(CityBindingModel model)
        {
            City element = context.Cities.FirstOrDefault(rec => rec.CityName ==
             model.CityName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = context.Cities.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CityName = model.CityName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            City element = context.Cities.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Cities.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
