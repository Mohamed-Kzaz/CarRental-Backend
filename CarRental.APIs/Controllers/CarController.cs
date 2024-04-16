using AutoMapper;
using CarRental.APIs.DTOs.Car;
using CarRental.APIs.Helper;
using CarRental.Core;
using CarRental.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var cars = await _unitOfWork.CarRepository.GetAllAsync();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var car = await _unitOfWork.CarRepository.GetByIdAsync(id);

            if (car == null)
                return NotFound("Car not found");

            return Ok(car);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCar([FromForm] CarDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carURL = DocumentSettings.UploadFile(model.CarImage, "images");

            Car car = new Car()
            {
                Brand = model.Brand,
                Model = model.Model,
                Year = model.Year,
                Color = model.Color,
                CarImageURL = carURL,
                Trans_Type = model.Trans_Type,
                Seats = model.Seats,
                Cost_Per_Day = model.Cost_Per_Day,
                IsAvailable = model.IsAvailable,
                OwnerId = model.OwnerId,
            };

            await _unitOfWork.CarRepository.Add(car);

            return Ok("Added Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var car = await _unitOfWork.CarRepository.GetByIdAsync(id);

            if (car == null)
                return NotFound("Car not found");

            DocumentSettings.DeleteFile(car.CarImageURL, "images");

            _unitOfWork.CarRepository.Delete(car);

            return Ok(car);
        }
    }
}
