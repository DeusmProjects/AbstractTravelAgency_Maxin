using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceDAL.ViewModel
{
    public class CityConditionViewModel
    {
        public int CityConditionId { get; set; }

        public int CityId { get; set; }

        public int ConditionId { get; set; }

        [DisplayName("Название условия")]
        public string ConditionName { get; set; }

        [DisplayName("Количество")]
        public int Amount { get; set; }
    }
}
