using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }

        [ForeignKey("Rental")]
        public int? RentalId { get; set; }
        public virtual Rental? Rental { get; set; }
    }
}
