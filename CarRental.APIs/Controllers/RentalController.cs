using CarRental.APIs.DTOs.Rental;
using CarRental.Core;
using CarRental.Core.Entities;
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

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] RentalDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var car = await _unitOfWork.CarRepository.GetByIdAsync(model.CarId);

            if(!car.IsAvailable is true)
                return BadRequest(new {message = "Car is not available now"});

            var totalDays = _unitOfWork.RentalRepository.GetTotalDays(model.Start_Date, model.End_Date);

            Rental rental = new Rental()
            {
                Start_Date = model.Start_Date,
                End_Date = model.End_Date,
                Total_Cost = car.Cost_Per_Day * totalDays,
                Pick_Location = model.Pick_Location,
                Ret_Location = model.Ret_Location,
                Pay_Date = DateTime.Now,
                Trans_Id = model.Trans_Id,
                CarId = model.CarId,
                ClientId = model.ClientId
            };

            await _unitOfWork.RentalRepository.Add(rental);

            await UpdateAvailability(model.CarId);

            return Ok(new { message = "Rental is added successfully", Availability = car.IsAvailable, RentalID = rental.Id});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAvailability(int id)
        {
            var car = await _unitOfWork.CarRepository.GetByIdAsync(id);

            car.IsAvailable = !car.IsAvailable;

            await _unitOfWork.CarRepository.Update(car);

            return Ok(car);
        }

    }
}
