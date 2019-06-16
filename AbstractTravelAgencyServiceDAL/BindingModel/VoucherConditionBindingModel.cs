using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceDAL.BindingModel
{
    public class VoucherConditionBindingModel
    {
        public int Id { get; set; }

        public int VoucherId { get; set; }

        public int ConditionId { get; set; }

        public int Amount { get; set; }
    }
}
