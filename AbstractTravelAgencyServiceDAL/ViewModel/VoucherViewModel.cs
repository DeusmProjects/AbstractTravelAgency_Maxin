using System.Collections.Generic;
using System.ComponentModel;

namespace AbstractTravelAgencyServiceDAL.ViewModel
{
    public class VoucherViewModel
    {
        public int VoucherId { get; set; }

        [DisplayName("Название путевки")]
        public string VoucherName { get; set; }

        [DisplayName("Цена")]
        public decimal Cost { get; set; }

        public List<VoucherConditionViewModel> VoucherConditions { get; set; }
    }
}
