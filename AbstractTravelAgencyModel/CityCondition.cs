using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyModel
{
    public class CityCondition
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public int ConditionId { get; set; }

        [Required]
        public int Amount { get; set; }

        public virtual City City { get; set; }
        public virtual Condition Condition { get; set; }
    }
}
