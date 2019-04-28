using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyModel
{
    [DataContract]
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [DataMember]
        public string Mail { get; set; }

        [DisplayName("ФИО Клиента")]
        public string CustomerFIO { get; set; }

        [DataMember]
        public List<InfoMessageViewModel> Messages { get; set; }
    }
}
