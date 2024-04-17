using CarRental.Core.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRental.APIs.DTOs.Car
{
    public class CarDto
    {
        [Required(ErrorMessage = "Brand is required!")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model is required!")]
        public string Modell { get; set; }

        [Range(2005, 2024, ErrorMessage = "Car model must be within range 2005 to 2024")]
        [Required(ErrorMessage = "Year is required!")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Color is required!")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Car Image is required!")]
        public IFormFile CarImage { get; set; }

        [Required(ErrorMessage = "Car transmission Type is required!")]
        [EnumDataType(typeof(CarTransType))]
        public CarTransType Trans_Type { get; set; }

        [Required(ErrorMessage = "Seats is required!")]
        public int Seats { get; set; }

        [Required(ErrorMessage = "Cost is required!")]
        public int Cost_Per_Day { get; set; }

        [Required(ErrorMessage = "IsAvailable is required!")]
        public bool IsAvailable { get; set; } 

        public string? OwnerId { get; set; } 
    }
}
