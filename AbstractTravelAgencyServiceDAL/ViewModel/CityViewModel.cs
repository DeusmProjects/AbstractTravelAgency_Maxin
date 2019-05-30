using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceDAL.ViewModel
{
    public class CityViewModel
    {
        public int CityId { get; set; }

        [DisplayName("Название города")]
        public string CityName { get; set; }

        public List<CityConditionViewModel> CityConditions { get; set; }
    }
}
