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
        public async Task<ActionResult<IReadOnlyList<ReviewToReturnDto>>> GetById(int id)
        {
            var reviews = await _unitOfWork.ReviewRepository.GetAllReviwsForCar(id);

            var mappedReviews = new List<ReviewToReturnDto>();

            foreach (var review in reviews)
            {
                var mappedReview = new ReviewToReturnDto
                {
                    Rating = review.Rating,
                    Comment = review.Comment,
                    UserName = review.Rental.User.UserName
                };

                mappedReviews.Add(mappedReview);
            }


            return Ok(mappedReviews);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ReviewDto model)
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
