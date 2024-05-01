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
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        private readonly CarRentalContext dbContext;

        public CarRepository(CarRentalContext _dbContext) : base(_dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<IList<Car>> GetAllCarsExceptOnwerCar(string ownerId)
        {
            return await dbContext.Cars.Include(c => c.User).Where(c => c.OwnerId != ownerId).ToListAsync();
        }

        public async Task<IList<Car>> GetAllCarsForOwner(string ownerId)
        {
            return await dbContext.Cars.Include(c => c.User).Where(c => c.OwnerId == ownerId).ToListAsync();
        }

        public async Task<Car> GetCarById(int carId)
        {
            return await dbContext.Cars.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == carId);
        }
    }
}
