using CarRental.Core.Entities;
using CarRental.Core.Repositories;
using CarRental.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Repository
{
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        public CarRepository(CarRentalContext _dbContext) : base(_dbContext)
        {
        }
    }
}
