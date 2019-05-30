using AbstractTravelAgencyModel;
using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;

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
                result.Add(new CustomerViewModel
                {
                    Id = source.Customers[i].Id,
                    CustomerFIO = source.Customers[i].CustomerFIO
                });
            }
        return result;
        }

        public CustomerViewModel GetElement(int id)
        {
            Customer element = source.Customers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                if (source.Customers[i].Id == id)
                {
                    return new CustomerViewModel
                    {
                        Id = source.Customers[i].Id,
                        CustomerFIO = source.Customers[i].CustomerFIO
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CustomerBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Customers.Count; ++i)
            {
                if (source.Customers[i].Id > maxId)
                {
                    maxId = source.Customers[i].Id;
                }
                if (source.Customers[i].CustomerFIO == model.CustomerFIO)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            source.Customers.Add(new Customer
            {
                CustomerId = maxId + 1,
                CustomerFIO = model.CustomerFIO
            });
        }

        public void UpdElement(CustomerBindingModel model)
        {
            Customer element = source.Customers.FirstOrDefault(rec => rec.CustomerFIO == 
            model.CustomerFIO && rec.Id != model.Id);
            if (element != null)
            {
                if (source.Customers[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Customers[i].CustomerFIO == model.CustomerFIO &&
                source.Customers[i].Id != model.Id)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            element.CustomerFIO = model.CustomerFIO;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Customers.Count; ++i)
            {
                if (source.Customers[i].Id == id)
                {
                    source.Customers.RemoveAt(i);
                    return;
                }
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
