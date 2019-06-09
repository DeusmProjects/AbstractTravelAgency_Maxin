using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyModel
{
    public class VoucherCondition
    {
        public int Id { get; set; }

        public int VoucherId { get; set; }

        public int ConditionId { get; set; }

        public int Amount { get; set; }
    }
}
