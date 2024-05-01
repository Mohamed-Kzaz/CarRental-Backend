using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string ImageProfileURl { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public string DrivingLicURl { get; set; }
        public string NationalIdURl { get; set; }
        public DateTime DOB { get; set; }

        // For Owner
        public virtual ICollection<Car>? Cars { get; set; } = new HashSet<Car>();

        // For Client
        public virtual ICollection<Rental>? Rentals { get; set; } = new HashSet<Rental>();

    }
}
