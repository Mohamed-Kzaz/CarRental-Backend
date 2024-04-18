using CarRental.Core.Entities;
using CarRental.Core.Repositories;
using CarRental.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Repository
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly CarRentalContext dbContext;

        public ReviewRepository(CarRentalContext _dbContext) : base(_dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<IReadOnlyList<Review>> GetAllReviwsForCar(int carId)
        {
            return await dbContext.Reviews.Where(r => r.Rental.CarId == carId).ToListAsync();
        }
    }
}
