using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceDAL.ViewModel
{
    [DataContract]
    public class BookingViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public string CustomerFIO { get; set; }

        [DataMember]
        public int VoucherId { get; set; }

        [DataMember]
        public string VoucherName { get; set; }

        [DataMember]
        public int? ExecutorId { get; set; }

        [DataMember]
        public string ExecutorName { get; set; }

        [DataMember]
        public int Amount { get; set; }

        [DataMember]
        public decimal TotalSum { get; set; }

        [DataMember]
        public string StatusBooking { get; set; }

        [DataMember]
        public string DateCreateBooking { get; set; }

        [DataMember]
        public string DateImplementBooking { get; set; }
    }
}
