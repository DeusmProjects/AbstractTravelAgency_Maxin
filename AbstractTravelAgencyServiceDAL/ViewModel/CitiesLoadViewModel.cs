using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceDAL.ViewModel
{
    public class CitiesLoadViewModel
    {
        public string CityName { get; set; }
        public int TotalAmount { get; set; }
        public IEnumerable<Tuple<string, int>> Conditions { get; set; }
    }
}
