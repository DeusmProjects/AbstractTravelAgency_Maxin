using AbstractTravelAgencyModel;
using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceImplement.Implementations
{
    public class CityServiceList : ICityService
    {
        private DataListSingleton source;

        public CityServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<CityViewModel> GetList()
        {
            List<CityViewModel> result = source.Cities
            .Select(rec => new CityViewModel
            {
                CityId = rec.CityId,
                CityName = rec.CityName,
                CityConditions = source.CityConditions.Where(recPC => recPC.CityId == rec.CityId)
                .Select(recPC => new CityConditionViewModel
                   {
                       CityConditionId = recPC.CityConditionId,
                       CityId = recPC.CityId,
                       ConditionId = recPC.ConditionId,
                       ConditionName = source.Conditions.FirstOrDefault(recC => recC.ConditionId == recPC.ConditionId)?.ConditionName,
                       Amount = recPC.Amount
                   }).ToList()
            }).ToList();
            return result;
        }

        public CityViewModel GetElement(int id)
        {
            City element = source.Cities.FirstOrDefault(rec => rec.CityId == id);
            if (element != null)
            {
                return new CityViewModel
                {
                    CityId = element.CityId,
                    CityName = element.CityName,
                    CityConditions = source.CityConditions.Where(recPC => recPC.CityId == element.CityId)
                    .Select(recPC => new CityConditionViewModel
                       {
                           CityConditionId = recPC.CityConditionId,
                           CityId = recPC.CityId,
                           ConditionId = recPC.ConditionId,
                           ConditionName = source.Conditions.FirstOrDefault(recC => recC.ConditionId == recPC.ConditionId)?.ConditionName,
                           Amount = recPC.Amount
                       }).ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CityBindingModel model)
        {
            City element = source.Cities.FirstOrDefault(rec => rec.CityName == model.CityName);
            if (element != null)
            {
                throw new Exception("Уже есть город с таким названием");
            }
            int maxId = source.Cities.Count > 0 ? source.Cities.Max(rec => rec.CityId) : 0;
            source.Cities.Add(new City
            {
                CityId = maxId + 1,
                CityName = model.CityName
            });
        }

        public void UpdElement(CityBindingModel model)
        {
            City element = source.Cities.FirstOrDefault(rec =>
            rec.CityName == model.CityName && rec.CityId != model.CityId);
            if (element != null)
            {
                throw new Exception("Уже есть город с таким названием");
            }
            element = source.Cities.FirstOrDefault(rec => rec.CityId == model.CityId);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CityName = model.CityName;
        }

        public void DelElement(int id)
        {
            City element = source.Cities.FirstOrDefault(rec => rec.CityId == id);
            if (element != null)
            {
                // при удалении удаляем все записи о компонентах на удаляемом городе
                source.CityConditions.RemoveAll(rec => rec.CityId == id);
                source.Cities.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
