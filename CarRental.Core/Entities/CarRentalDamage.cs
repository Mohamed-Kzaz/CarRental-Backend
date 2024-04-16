using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Entities
{
    public class CarRentalDamage
    {
        public int CarId { get; set; }
        public int RentId { get; set; }
        public int DmgId { get; set; }

        public virtual Car Car { get; set; }
        public virtual Rental Rental { get; set; }
        public virtual DamageReport DamageReport { get; set; }
    }
}
