using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceDAL.ViewModel
{
    public class ConditionViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название условия")]
        public string ConditionName { get; set; }
    }
}
