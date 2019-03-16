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
    public class VoucherServiceList : IVoucherService
    {
        private DataListSingleton source;
        public VoucherServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<VoucherViewModel> GetList()
        {

            List<VoucherViewModel> result = source.Vouchers
                .Select(rec => new VoucherViewModel
                {
                    Id = rec.Id,
                    VoucherName = rec.VoucherName,
                    Cost = rec.Cost,
                    VoucherCondition = source.VoucherConditions
                        .Where(recPC => recPC.VoucherId == rec.Id)
                        .Select(recPC => new VoucherConditionViewModel
                        {
                            Id = recPC.Id,
                            VoucherId = recPC.VoucherId,
                            ConditionId = recPC.ConditionId,
                            ConditionName = source.Conditions.FirstOrDefault(recC =>
                            recC.Id == recPC.ConditionId)?.ConditionName,
                            Amount = recPC.Amount
                        })
                        .ToList()
                })
                .ToList();
            return result;
        }
        public VoucherViewModel GetElement(int id)
        {
            Voucher element = source.Vouchers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new VoucherViewModel
                {
                    Id = element.Id,
                    VoucherName = element.VoucherName,
                    Cost = element.Cost,
                    VoucherCondition = source.VoucherConditions
                        .Where(recPC => recPC.VoucherId == element.Id)
                        .Select(recPC => new VoucherConditionViewModel
                        {
                            Id = recPC.Id,
                            VoucherId = recPC.VoucherId,
                            ConditionId = recPC.ConditionId,
                            ConditionName = source.Conditions.FirstOrDefault(recC => recC.Id == recPC.ConditionId)?.ConditionName,
                            Amount = recPC.Amount
                        })
                       .ToList()
                };
            }
            throw new Exception("Элемент не найден");

        }

        public void AddElement(VoucherBindingModel model)
        {
            Voucher element = source.Vouchers.FirstOrDefault(rec => rec.VoucherName == model.VoucherName);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.Vouchers.Count > 0 ? source.Vouchers.Max(rec => rec.Id) : 0;
            source.Vouchers.Add(new Voucher
            {
                Id = maxId + 1,
                VoucherName = model.VoucherName,
                Cost = model.Cost
            });
            // компоненты для изделия
            int maxPCId = source.VoucherConditions.Count > 0 ? source.VoucherConditions.Max(rec => rec.Id) : 0;
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
                    Id = ++maxPCId,
                    VoucherId = maxId + 1,
                    ConditionId = groupCondition.ConditionId,
                    Amount = groupCondition.Amount
                });
            }
        }

        public void UpdElement(VoucherBindingModel model)
        {
            Voucher element = source.Vouchers.FirstOrDefault(rec => rec.VoucherName == 
            model.VoucherName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть путевка с таким названием");
            }
            element = source.Vouchers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.VoucherName = model.VoucherName;
            element.Cost = model.Cost;
            int maxPCId = source.VoucherConditions.Count > 0 ? source.VoucherConditions.Max(rec => rec.Id) : 0;
            // обновляем существуюущие компоненты
            var compIds = model.VoucherConditions.Select(rec => rec.ConditionId).Distinct();
            var updateConditions = source.VoucherConditions.Where(rec => rec.VoucherId ==
             model.Id && compIds.Contains(rec.ConditionId));
            foreach (var updateCondition in updateConditions)
            {
                updateCondition.Amount = model.VoucherConditions.FirstOrDefault(rec => rec.Id == updateCondition.Id).Amount;
            }
            source.VoucherConditions.RemoveAll(rec => rec.VoucherId == model.Id && !compIds.Contains(rec.ConditionId));
            // новые записи
            var groupConditions = model.VoucherConditions
                                    .Where(rec => rec.Id == 0)
                                    .GroupBy(rec => rec.ConditionId)
                                    .Select(rec => new
                                    {
                                        ConditionId = rec.Key,
                                        Amount = rec.Sum(r => r.Amount)
                                    });
            foreach (var groupCondition in groupConditions)
            {
                VoucherCondition elementPC = source.VoucherConditions.FirstOrDefault(rec => 
                    rec.VoucherId == model.Id && rec.ConditionId == groupCondition.ConditionId);
                if (elementPC != null)
                {
                    elementPC.Amount += groupCondition.Amount;
                }
                else
                {
                    source.VoucherConditions.Add(new VoucherCondition
                    {
                        Id = ++maxPCId,
                        VoucherId = model.Id,
                        ConditionId = groupCondition.ConditionId,
                        Amount = groupCondition.Amount
                    });
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
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
