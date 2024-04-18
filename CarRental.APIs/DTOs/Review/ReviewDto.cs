using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.APIs.DTOs.Review
{
    public class ReviewDto
    {
        [Range(0,5)]
        public int? Rating { get; set; }

        [MaxLength(100)]
        public string? Comment { get; set; }

        public int RentalId { get; set; }
    }
}
