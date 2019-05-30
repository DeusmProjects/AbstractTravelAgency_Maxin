using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceDAL.BindingModel
{
    public class VoucherBindingModel
    {
        public int VoucherId { get; set; }

        public string VoucherName { get; set; }

        public decimal Cost { get; set; }

        public List<VoucherConditionBindingModel> VoucherConditions { get; set; }
    }
}
