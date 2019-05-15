using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceDAL.ViewModel
{
    public class CustomerBookingsModel
    {
        public string CustomerName { get; set; }
        public string DateCreateBooking { get; set; }
        public string VoucherName { get; set; }
        public int Amount { get; set; }
        public decimal TotalSum { get; set; }
        public string StatusBooking { get; set; }
    }
}
