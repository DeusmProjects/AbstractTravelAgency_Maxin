using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceDAL.BindingModel
{
    public class CityConditionBindingModel
    {
        public int CityConditionId { get; set; }

        public int CityId { get; set; }

        public int ConditionId { get; set; }

        public int Amount { get; set; }
    }
}
