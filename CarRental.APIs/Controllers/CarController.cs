using AutoMapper;
using CarRental.APIs.DTOs.Car;
using CarRental.APIs.Helper;
using CarRental.Core;
using CarRental.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllCarsByOwnerId(string id)
        {
            var cars = await _unitOfWork.CarRepository.GetAllCarsForOwner(id);

            if (cars == null)
            {
                return NotFound(new { message = "you have not added cars yet" });
            }
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";

            foreach (var car in cars)
            {
                car.CarImageURL = baseUrl + car.CarImageURL;
            }
            return Ok(cars);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var cars = await _unitOfWork.CarRepository.GetAllAsync();

            if (cars == null)
            {
                return NotFound(new { message = "Cars are not found" });
            }

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";

            foreach (var car in cars)
            {
                car.CarImageURL = baseUrl + car.CarImageURL;
            }

            return Ok(cars);
        }

        [HttpGet("getallcars/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var car = await _unitOfWork.CarRepository.GetByIdAsync(id);

            if (car == null)
                return NotFound(new { message = "Car are not found" });

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";

            car.CarImageURL = baseUrl + car.CarImageURL;

            return Ok(car);
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCar([FromForm] CarDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carURL = DocumentSettings.UploadFile(model.CarImage, "images");

            Car car = new Car()
            {
                Brand = model.Brand,
                Model = model.Modell,
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

            return Ok(new { message = "Car is added successfully" });
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] CarDto model)
        {
            var car = await _unitOfWork.CarRepository.GetByIdAsync(id);

            if (car == null)
                return NotFound("No car was found with ID");

            var carURL = DocumentSettings.UploadFile(model.CarImage, "images");

            car.Brand = model.Brand;
            car.Model = model.Modell;
            car.Year = model.Year;
            car.Color = model.Color;
            car.CarImageURL = carURL;
            car.Trans_Type = model.Trans_Type;
            car.Seats = model.Seats;
            car.Cost_Per_Day = model.Cost_Per_Day;
            car.IsAvailable = model.IsAvailable;
            car.OwnerId = car.OwnerId;

            await _unitOfWork.CarRepository.Update(car);

            return Ok(new { message = "Car is updated successfully", car = car });
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var car = await _unitOfWork.CarRepository.GetByIdAsync(id);

            if (car == null)
                return NotFound("Car not found");

            DocumentSettings.DeleteFile(car.CarImageURL, "images");

            await _unitOfWork.CarRepository.Delete(car);

            return Ok(car);
        }
    }
}
