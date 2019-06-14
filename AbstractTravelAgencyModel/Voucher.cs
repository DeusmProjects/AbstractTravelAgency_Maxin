using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyModel
{
    public class Voucher
    {
        public int Id { get; set; }

        [Required]
        public string VoucherName { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [ForeignKey("VoucherId")]
        public virtual List<VoucherCondition> VoucherConditions { get; set; }

        [ForeignKey("VoucherId")]
        public virtual List<Booking> Bookings { get; set; }
    }
}
