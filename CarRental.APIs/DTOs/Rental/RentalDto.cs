using System.ComponentModel.DataAnnotations;

namespace CarRental.APIs.DTOs.Rental
{
    public class RentalDto
    {
        [Required(ErrorMessage = "Start Date is required!")]
        public DateTime Start_Date { get; set; }

        [Required(ErrorMessage = "End Date is required!")]
        public DateTime End_Date { get; set; }

        [Required(ErrorMessage = "Pick Location is required!")]
        public string Pick_Location { get; set; }

        [Required(ErrorMessage = "Ret Location is required!")]
        public string Ret_Location { get; set; }
        public string ClientId { get; set; }
        public int CarId { get; set; }
    }
}
