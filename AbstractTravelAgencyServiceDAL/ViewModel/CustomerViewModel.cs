using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceDAL.ViewModel
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }

        [DisplayName("ФИО Клиента")]
        public string CustomerFIO { get; set; }
    }
}
