using CarRental.Core.Entities.Enum;

namespace CarRental.APIs.DTOs.Rental
{
    public class RequestsForCarOwner
    {
        public int Id { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string Pick_Location { get; set; }
        public string Ret_Location { get; set; }
        public decimal TotalCost { get; set; }
        public RentalStatus Status { get; set; }
        public int RentalDays { get; set; }

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string DrivingLic { get; set; }
    }
}
