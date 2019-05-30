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
            List<CustomerViewModel> result = new List<CustomerViewModel>();
            for (int i = 0; i < source.Customers.Count; ++i)
            {
                result.Add(new CustomerViewModel
                {
                    CustomerId = source.Customers[i].CustomerId,
                    CustomerFIO = source.Customers[i].CustomerFIO
                });
            }
        return result;
        }

        public CustomerViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Customers.Count; ++i)
            {
                if (source.Customers[i].CustomerId == id)
                {
                    return new CustomerViewModel
                    {
                        CustomerId = source.Customers[i].CustomerId,
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
                if (source.Customers[i].CustomerId > maxId)
                {
                    maxId = source.Customers[i].CustomerId;
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
            int index = -1;
            for (int i = 0; i < source.Customers.Count; ++i)
            {
                if (source.Customers[i].CustomerId == model.CustomerId)
                {
                    index = i;
                }
                if (source.Customers[i].CustomerFIO == model.CustomerFIO &&
                source.Customers[i].CustomerId != model.CustomerId)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Customers[index].CustomerFIO = model.CustomerFIO;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Customers.Count; ++i)
            {
                if (source.Customers[i].CustomerId == id)
                {
                    source.Customers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
