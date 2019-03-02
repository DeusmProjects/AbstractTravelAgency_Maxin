using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceDAL.BindingModel
{
    public class BookingBindingModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int VoucherId { get; set; }
        public int Amount { get; set; }
        public decimal TotalSum { get; set; }
    }
}
