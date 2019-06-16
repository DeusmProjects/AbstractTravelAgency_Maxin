using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyModel
{
    public class Condition
    {
        public int Id { get; set; }

        [Required]
        public string ConditionName { get; set; }

        [ForeignKey("ConditionId")]
        public virtual List<VoucherCondition> VoucherConditions { get; set; }

        [ForeignKey("ConditionId")]
        public virtual List<CityCondition> CityConditions { get; set; }
    }
}
