using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceDAL.BindingModel
{
    [DataContract]
    public class BookingBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public int VoucherId { get; set; }

        [DataMember]
        public int? ExecutorId { get; set; }

        [DataMember]
        public int Amount { get; set; }

        [DataMember]
        public decimal TotalSum { get; set; }
    }
}
