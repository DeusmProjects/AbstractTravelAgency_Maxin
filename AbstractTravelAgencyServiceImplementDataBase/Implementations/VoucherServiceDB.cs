using AbstractTravelAgencyModel;
using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceImplementDataBase.Implementations
{
    public class VoucherServiceDB : IVoucherService
    {
        private AbstractDbScope context;
        public VoucherServiceDB(AbstractDbScope context)
        {
            this.context = context;
        }
        public List<VoucherViewModel> GetList()
        {
            List<VoucherViewModel> result = context.Vouchers.Select(rec => new VoucherViewModel
            {
                Id = rec.Id,
                VoucherName = rec.VoucherName,
                Cost = rec.Cost,
                VoucherConditions = context.VoucherConditions
            .Where(recPC => recPC.VoucherId == rec.Id)
           .Select(recPC => new VoucherConditionViewModel
               {
                   Id = recPC.Id,
                   VoucherId = recPC.VoucherId,
                   ConditionId = recPC.ConditionId,
                   ConditionName = recPC.Condition.ConditionName,
                   Amount = recPC.Amount
               })
           .ToList()
            })
            .ToList();
            return result;
        }
        public VoucherViewModel GetElement(int id)
        {
            Voucher element = context.Vouchers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new VoucherViewModel
            {
                    Id = element.Id,
                    VoucherName = element.VoucherName,
                    Cost = element.Cost,
                    VoucherConditions = context.VoucherConditions
                     .Where(recPC => recPC.VoucherId == element.Id)
                     .Select(recPC => new VoucherConditionViewModel
                     {
                         Id = recPC.Id,
                         VoucherId = recPC.VoucherId,
                         ConditionId = recPC.ConditionId,
                         ConditionName = recPC.Condition.ConditionName,
                         Amount = recPC.Amount
                     })
                     .ToList()
                     };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(VoucherBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Voucher element = context.Vouchers.FirstOrDefault(rec =>
                   rec.VoucherName == model.VoucherName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть путевка с таким названием");
                    }
                    element = new Voucher
                    {
                        VoucherName = model.VoucherName,
                        Cost = model.Cost
                    };
                    context.Vouchers.Add(element);
                    context.SaveChanges();
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
                        context.VoucherConditions.Add(new VoucherCondition
                        {
                            VoucherId = element.Id,
                            ConditionId = groupCondition.ConditionId,
                            Amount = groupCondition.Amount
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void UpdElement(VoucherBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Voucher element = context.Vouchers.FirstOrDefault(rec =>
                   rec.VoucherName == model.VoucherName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть путевка с таким названием");
                    }
                    element = context.Vouchers.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.VoucherName = model.VoucherName;
                    element.Cost = model.Cost;
                    context.SaveChanges();
                    // обновляем существуюущие компоненты
                    var compIds = model.VoucherConditions.Select(rec =>
                   rec.ConditionId).Distinct();
                    var updateConditions = context.VoucherConditions.Where(rec =>
                   rec.VoucherId == model.Id && compIds.Contains(rec.ConditionId));
                    foreach (var updateCondition in updateConditions)
                    {
                        updateCondition.Amount =
                       model.VoucherConditions.FirstOrDefault(rec => rec.Id == updateCondition.Id).Amount;
                    }
                    context.SaveChanges();
                    context.VoucherConditions.RemoveRange(context.VoucherConditions.Where(rec =>
                    rec.VoucherId == model.Id && !compIds.Contains(rec.ConditionId)));
                    context.SaveChanges();
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
                        VoucherCondition elementPC =
                       context.VoucherConditions.FirstOrDefault(rec => rec.VoucherId == model.Id &&
                       rec.ConditionId == groupCondition.ConditionId);
                        if (elementPC != null)
                        {
                            elementPC.Amount += groupCondition.Amount;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.VoucherConditions.Add(new VoucherCondition
                            {
                                VoucherId = model.Id,
                            ConditionId = groupCondition.ConditionId,
                                Amount = groupCondition.Amount
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Voucher element = context.Vouchers.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.VoucherConditions.RemoveRange(context.VoucherConditions.Where(rec =>
                        rec.VoucherId == id));
                        context.Vouchers.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
