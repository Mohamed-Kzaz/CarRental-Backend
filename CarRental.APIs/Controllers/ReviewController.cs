using CarRental.APIs.DTOs.Review;
using CarRental.APIs.Helper;
using CarRental.Core;
using CarRental.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<Review>>> GetById(int id)
        {
            var reviws = await _unitOfWork.ReviewRepository.GetAllReviwsForCar(id);

            return Ok(reviws);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] ReviewDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Review review = new Review()
            {
                Rating = model.Rating,
                Comment = model.Comment,
                RentalId = model.RentalId,
            };

            await _unitOfWork.ReviewRepository.Add(review);

            return Ok(new { message = "Review is submitted" });
        }

    }
}
