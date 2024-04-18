﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Entities
{
    public class Rental
    {
        public int Id { get; set; }
        public int Pay_Id { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public int Total_Cost { get; set; }
        public string Pick_Location { get; set; }
        public string Ret_Location { get; set; }
        public DateTime Pay_Date { get; set; } = DateTime.Now;
        public int Trans_Id { get; set; }

        [ForeignKey("User")]
        public string ClientId { get; set; }
        public virtual ApplicationUser User { get; set; }


        [ForeignKey("Car")]
        public int CarId { get; set; }
        public virtual Car Car { get; set; }

        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

    }
}
