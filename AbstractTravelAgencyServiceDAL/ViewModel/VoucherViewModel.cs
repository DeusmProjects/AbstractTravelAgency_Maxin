using AbstractTravelAgencyServiceDAL.BindingModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyModel
{
    public class VoucherViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название продукта")]
        public string VoucherName { get; set; }

        [DisplayName("Цена")]
        public decimal Cost { get; set; }

        public List<VoucherConditionViewModel> VoucherConditions { get; set; }
        public List<VoucherConditionViewModel> VoucherCondition { get; set; }
    }
}
