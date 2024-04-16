using CarRental.Core.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Entities
{
    public class DamageReport
    {
        public int Id { get; set; }
        public string Dam_Desc { get; set; }
        public int Repair_Cost { get; set; }
        public DateTime Report_Date { get; set; }
        public DamageStatus Status { get; set; }

        public virtual ICollection<CarRentalDamage> CarRentalDamages { get; set; } = new HashSet<CarRentalDamage>();
    }
}
