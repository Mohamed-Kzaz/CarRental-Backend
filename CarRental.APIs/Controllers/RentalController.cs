using CarRental.APIs.DTOs.Rental;
using CarRental.APIs.DTOs.Review;
using CarRental.Core;
using CarRental.Core.Entities;
using CarRental.Core.Entities.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RentalController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllRentalsByClientId(string id)
        {
            var rentals = await _unitOfWork.RentalRepository.GetAllRentalsForClient(id);

            if (rentals == null)
            {
                return NotFound(new { message = "Haven't rented a car yet" });
            }

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";

            var mappedRentals = new List<RentalToReturnDto>();

            foreach (var rental in rentals)
            {
                var mappedRental = new RentalToReturnDto
                {
                    Start_Date = rental.Start_Date,
                    End_Date = rental.End_Date,
                    Total_Cost = rental.Total_Cost,
                    Pick_Location = rental.Pick_Location,
                    Ret_Location = rental.Ret_Location,
                    Status = rental.Status,
                    CarImageURL = baseUrl + rental.Car.CarImageURL
                };

                mappedRentals.Add(mappedRental);
            }
            return Ok(mappedRentals);
        }

        [HttpGet("getAllReqForCarById/{id}")]
        public async Task<ActionResult<RequestsForCarOwner>> GetAllReqForCarById(int id)
        {
            var requests = await _unitOfWork.RentalRepository.GetAllReqForCarById(id);

            if (requests == null)
            {
                return NotFound(new { message = "This car has no requests" });
            }

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";

            var mappedRequests = new List<RequestsForCarOwner>();

            foreach (var request in requests)
            {
                var mappedRequest = new RequestsForCarOwner
                {
                    Id = request.Id,
                    Start_Date = request.Start_Date,
                    End_Date = request.End_Date,
                    Pick_Location = request.Pick_Location,
                    Ret_Location = request.Ret_Location,
                    TotalCost = request.Total_Cost,
                    RentalDays = _unitOfWork.RentalRepository.GetTotalDays(request.Start_Date, request.End_Date),
                    Status = request.Status,

                    FullName = request.User.FName + " " + request.User.FName,
                    PhoneNumber = request.User.PhoneNumber,
                    Address = request.User.Address,
                    DrivingLic = baseUrl + request.User.DrivingLicURl
                };

                mappedRequests.Add(mappedRequest);
            }
            return Ok(mappedRequests);
        }


        [HttpPost]
        public async Task<IActionResult> Add(RentalDto rentalDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var car = await _unitOfWork.CarRepository.GetByIdAsync(rentalDto.CarId);

            if (!car.IsAvailable is true)
                return BadRequest(new { message = "Car is not available now" });

            var totalCost = GetTotalPrice(car.Cost_Per_Day, rentalDto.Start_Date, rentalDto.End_Date);

            Rental rental = new Rental()
            {
                Start_Date = rentalDto.Start_Date,
                End_Date = rentalDto.End_Date,
                Pick_Location = rentalDto.Pick_Location,
                Ret_Location = rentalDto.Ret_Location,
                Total_Cost = totalCost,
                Status = RentalStatus.Pending,
                CarId = rentalDto.CarId,
                ClientId = rentalDto.ClientId,
            };

            await _unitOfWork.RentalRepository.Add(rental);

            return Ok(new { message = "Rental is added successfully", RentalID = rental.Id, });
        }

        [HttpGet("getTotalPrice")]
        public int GetTotalPrice(int costPerDay, DateTime start, DateTime end)
        {
            var totalDays = _unitOfWork.RentalRepository.GetTotalDays(start, end);

            var totalCost = totalDays * costPerDay;

            return totalCost;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAvailability(int id)
        {
            var car = await _unitOfWork.CarRepository.GetByIdAsync(id);

            car.IsAvailable = !car.IsAvailable;

            await _unitOfWork.CarRepository.Update(car);

            return Ok(new { Car = car, message = "success" });
        }

        [HttpPut("RejectRequest/{id}")]
        public async Task<IActionResult> RejectRequest(int id)
        {
            var rental = await _unitOfWork.RentalRepository.GetByIdAsync(id);

            rental.Status = RentalStatus.Rejected;

            await _unitOfWork.RentalRepository.Update(rental);

            return Ok(new { message = "success" });
        }

        [HttpPut("PaymentRequest/{id}")]
        public async Task<IActionResult> PaymentRequest(int id)
        {
            var rental = await _unitOfWork.RentalRepository.GetByIdAsync(id);

            rental.Status = RentalStatus.WaitingForPayment;

            await _unitOfWork.RentalRepository.Update(rental);

            return Ok(new { message = "success" });
        }

        [HttpPut("ConfirmRequest/{id}")]
        public async Task<IActionResult> ConfirmRequest(int id)
        {
            var rental = await _unitOfWork.RentalRepository.GetByIdAsync(id);

            rental.Status = RentalStatus.Confirmed;

            await _unitOfWork.RentalRepository.Update(rental);

            await UpdateAvailability(rental.CarId);

            var rentals = await _unitOfWork.RentalRepository.GetAllReqPendAndRejForCarById(rental.CarId);

            foreach (var rent in rentals)
            {
                await _unitOfWork.RentalRepository.Delete(rent);
            }

            return Ok(new { message = "success" });
        }

    }
}
