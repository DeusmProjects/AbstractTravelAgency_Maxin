using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyModel
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        public string CityName { get; set; }

        [ForeignKey("CityId")]
        public virtual List<CityCondition> CityConditions { get; set; }
    }
}
