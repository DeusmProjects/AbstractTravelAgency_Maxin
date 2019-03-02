using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyModel
{
    public class VoucherConditionViewModel
    {
        public int Id { get; set; }

        public int VoucherId { get; set; }

        public int ConditionId { get; set; }

        [DisplayName("Условие")]
        public string ConditionName { get; set; }

        [DisplayName("Количество")]
        public int Amount { get; set; }
    }
}
