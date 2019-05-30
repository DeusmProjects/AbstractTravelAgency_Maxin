using AbstractTravelAgencyModel;
using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;

namespace AbstractTravelAgencyServiceImplement.Implementations
{
    public class VoucherServiceList : IVoucherService
    {
        private DataListSingleton source;

        public VoucherServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<VoucherViewModel> GetList()
        {
            List<VoucherViewModel> result = new List<VoucherViewModel>();
            for (int i = 0; i < source.Vouchers.Count; ++i)
            {
                List<VoucherConditionViewModel> voucherConditions = new List<VoucherConditionViewModel>();
                for (int j = 0; j < source.VoucherConditions.Count; ++j)
                {
                    if (source.VoucherConditions[j].VoucherId == source.Vouchers[i].Id)
                    {
                        string conditionName = string.Empty;
                        for (int k = 0; k < source.Conditions.Count; ++k)
                        {
                            if (source.VoucherConditions[j].ConditionId == source.Conditions[k].Id)
                            {
                                conditionName = source.Conditions[k].ConditionName;
                                break;
                            }
                        }
                        voucherConditions.Add(new VoucherConditionViewModel
                        {
                            Id = source.VoucherConditions[j].Id,
                            VoucherId = source.VoucherConditions[j].VoucherId,
                            ConditionId = source.VoucherConditions[j].ConditionId,
                            ConditionName = conditionName,
                            Amount = source.VoucherConditions[j].Amount
                        });
                    }
                }
                result.Add(new VoucherViewModel
                {
                    Id = source.Vouchers[i].Id,
                    VoucherName = source.Vouchers[i].VoucherName,
                    Cost = source.Vouchers[i].Cost,
                    VoucherCondition = voucherConditions
                });
            }
            return result;
        }

        public VoucherViewModel GetElement(int id)
        {
            Voucher element = source.Vouchers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                List<VoucherConditionViewModel> voucherConditions = new List<VoucherConditionViewModel>();
                for (int j = 0; j < source.VoucherConditions.Count; ++j)
                {
                    if (source.VoucherConditions[j].VoucherId == source.Vouchers[i].Id)
                    {
                        string conditionName = string.Empty;
                        for (int k = 0; k < source.Conditions.Count; ++k)
                        {
                            if (source.VoucherConditions[j].ConditionId == source.Conditions[k].Id)
                            {
                                conditionName = source.Conditions[k].ConditionName;
                                break;
                            }
                        }
                        voucherConditions.Add(new VoucherConditionViewModel
                        {
                            Id = source.VoucherConditions[j].Id,
                            VoucherId = source.VoucherConditions[j].VoucherId,
                            ConditionId = source.VoucherConditions[j].ConditionId,
                            ConditionName = conditionName,
                            Amount = source.VoucherConditions[j].Amount
                        });
                    }
                }
                if (source.Vouchers[i].Id == id)
                {
                    return new VoucherViewModel
                    {
                        Id = source.Vouchers[i].Id,
                        VoucherName = source.Vouchers[i].VoucherName,
                        Cost = source.Vouchers[i].Cost,
                        VoucherCondition = voucherConditions
                    };
                }
            }
            throw new Exception("Элемент не найден");

        }

        public void AddElement(VoucherBindingModel model)
        {
            Voucher element = source.Vouchers.FirstOrDefault(rec => rec.VoucherName == model.VoucherName);
            if (element != null)
            {
                if (source.Vouchers[i].Id > maxId)
                {
                    maxId = source.Vouchers[i].Id;
                }
                if (source.Vouchers[i].VoucherName == model.VoucherName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.Vouchers.Add(new Voucher
            {
                VoucherId = maxId + 1,
                VoucherName = model.VoucherName,
                Cost = model.Cost
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.VoucherConditions.Count; ++i)
            {
                if (source.VoucherConditions[i].Id > maxPCId)
                {
                    maxPCId = source.VoucherConditions[i].Id;
                }
            }
            // убираем дубли по компонентам
            var groupConditions = model.VoucherConditions
                                  .GroupBy(rec => rec.ConditionId)
                                  .Select(rec => new
                                   {
                                       ConditionId = rec.Key,
                                       Amount = rec.Sum(r => r.Amount)
                                   });
            // добавляем компоненты
            foreach (var groupCondition in groupConditions)
            {
                source.VoucherConditions.Add(new VoucherCondition
                {
                    VoucherConditionId = ++maxPCId,
                    VoucherId = maxId + 1,
                    ConditionId = groupCondition.ConditionId,
                    Amount = groupCondition.Amount
                });
            }
        }

        public void UpdElement(VoucherBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Vouchers.Count; ++i)
            {
                if (source.Vouchers[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Vouchers[i].VoucherName == model.VoucherName && source.Vouchers[i].Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            element = source.Vouchers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            source.Vouchers[index].VoucherName = model.VoucherName;
            source.Vouchers[index].Cost = model.Cost;
            int maxPCId = 0;
            for (int i = 0; i < source.VoucherConditions.Count; ++i)
            {
                if (source.VoucherConditions[i].Id > maxPCId)
                {
                    maxPCId = source.VoucherConditions[i].Id;
                }
            }
            // обновляем существуюущие компоненты
            for (int i = 0; i < source.VoucherConditions.Count; ++i)
            {
                if (source.VoucherConditions[i].VoucherId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.VoucherConditions.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.VoucherConditions[i].Id == model.VoucherConditions[j].Id)
                        {
                            source.VoucherConditions[i].Amount = model.VoucherConditions[j].Amount;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.VoucherConditions.RemoveAt(i--);
                    }
                }
            }
            source.VoucherConditions.RemoveAll(rec => rec.VoucherId == model.Id && !compIds.Contains(rec.ConditionId));
            // новые записи
            for (int i = 0; i < model.VoucherConditions.Count; ++i)
            {
                if (model.VoucherConditions[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.VoucherConditions.Count; ++j)
                    {
                        if (source.VoucherConditions[j].VoucherId == model.Id &&
                        source.VoucherConditions[j].ConditionId == model.VoucherConditions[i].ConditionId)
                        {
                            source.VoucherConditions[j].Amount += model.VoucherConditions[i].Amount;
                            model.VoucherConditions[i].Id = source.VoucherConditions[j].Id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.VoucherConditions[i].Id == 0)
                    {
                        source.VoucherConditions.Add(new VoucherCondition
                        {
                            Id = ++maxPCId,
                            VoucherId = model.Id,
                            ConditionId = model.VoucherConditions[i].ConditionId,
                            Amount = model.VoucherConditions[i].Amount
                        });
                    }
                }
            }
        }

        public void DelElement(int id)
        {
            Voucher element = source.Vouchers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.VoucherConditions.RemoveAll(rec => rec.VoucherId == id);
                source.Vouchers.Remove(element);
            }
            for (int i = 0; i < source.Vouchers.Count; ++i)
            {
                if (source.Vouchers[i].Id == id)
                {
                    source.Vouchers.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
