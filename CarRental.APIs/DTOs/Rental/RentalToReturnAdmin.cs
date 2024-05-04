using CarRental.Core.Entities.Enum;

namespace CarRental.APIs.DTOs.Rental
{
    public class RentalToReturnAdmin
    {
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public decimal Total_Cost { get; set; }
        public string Pick_Location { get; set; }
        public string Ret_Location { get; set; }
        public RentalStatus Status { get; set; }

        public string  FullNameCarOwner { get; set; }
        public string  FullNameClient { get; set; }
    }
}
