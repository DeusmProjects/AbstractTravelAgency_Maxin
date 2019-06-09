using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyModel
{
    public class Booking
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int VoucherId { get; set; }

        public int Amount { get; set; }

        public decimal TotalSum { get; set; }

        public BookingStatus StatusBooking { get; set; }

        public DateTime DateCreateBooking { get; set; }

        public DateTime? DateImplementBooking { get; set; }
    }
}
