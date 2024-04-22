namespace CarRental.APIs.DTOs.Rental
{
    public class RentalToReturnDto
    {
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public int Total_Cost { get; set; }
        public string Pick_Location { get; set; }
        public string Ret_Location { get; set; }
        public string CarImageURL { get; set; }
    }
}
