using AbstractTravelAgencyModel;
using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractTravelAgencyServiceImplement.Implementations
{
    public class CustomerServiceList : ICustomerService
    {
        private DataListSingleton source;

        public CustomerServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<CustomerViewModel> GetList()
        {
            List<CustomerViewModel> result = source.Customers.Select(rec => new CustomerViewModel
            {
                CustomerId = rec.CustomerId,
                CustomerFIO = rec.CustomerFIO
            })
            .ToList();
            return result;
        }

        public CustomerViewModel GetElement(int id)
        {
            Customer element = source.Customers.FirstOrDefault(rec => rec.CustomerId == id);
            if (element != null)
            {
                return new CustomerViewModel
                {
                    CustomerId = element.CustomerId,
                    CustomerFIO = element.CustomerFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CustomerBindingModel model)
        {
            Customer element = source.Customers.FirstOrDefault(rec => rec.CustomerFIO == model.CustomerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            int maxId = source.Customers.Count > 0 ? source.Customers.Max(rec => rec.CustomerId) : 0;
            source.Customers.Add(new Customer
            {
                CustomerId = maxId + 1,
                CustomerFIO = model.CustomerFIO
            });
        }

        public void UpdElement(CustomerBindingModel model)
        {
            Customer element = source.Customers.FirstOrDefault(rec => rec.CustomerFIO ==
            model.CustomerFIO && rec.CustomerId != model.CustomerId);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = source.Customers.FirstOrDefault(rec => rec.CustomerId == model.CustomerId);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CustomerFIO = model.CustomerFIO;
        }

        public void DelElement(int id)
        {
            Customer element = source.Customers.FirstOrDefault(rec => rec.CustomerId == id);
            if (element != null)
            {
                source.Customers.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
