using CarRental.Core.Entities;
using CarRental.Core.Entities.Enum;
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
    public class RentalRepository : GenericRepository<Rental>, IRentalRepository
    {

        public RentalRepository(CarRentalContext _dbContext) : base(_dbContext)
        {
        }

        public int GetTotalDays(DateTime startDate, DateTime endDate)
        {
            TimeSpan span = endDate - startDate;

            return span.Days + 1;
        }

        public async Task<IList<Rental>> GetAllRentalsForClient(string clientId)
        {
            return await dbContext.Rentals.Include(r => r.Car).Where(c => c.ClientId == clientId).ToListAsync();
        }

        public async Task<IList<Rental>> GetAllReqForCarById(int carId)
        {
            return await dbContext.Rentals.Include(r => r.User).Where(r => r.CarId == carId).ToListAsync();
        }

        public async Task<IList<Rental>> GetAllReqPendAndRejForCarById(int carId)
        {
            return await dbContext.Rentals.Where(r => r.CarId == carId && r.Status == RentalStatus.Pending || r.Status == RentalStatus.Rejected).ToListAsync();
        }

        public async Task<IList<Rental>> GetAllPendingRentals()
        {
            return await dbContext.Rentals.Include(r => r.User).Include(r => r.Car).ThenInclude(c => c.User).Where(r => r.Status == RentalStatus.Pending).ToListAsync();
        }

        public async Task<IList<Rental>> GetAllWaitingRentals()
        {
            return await dbContext.Rentals.Include(r => r.User).Include(r => r.Car).ThenInclude(c => c.User).Where(r => r.Status == RentalStatus.WaitingForPayment).ToListAsync();
        }

        public async Task<IList<Rental>> GetAllConfirmedRentals()
        {
            return await dbContext.Rentals.Include(r => r.User).Include(r => r.Car).ThenInclude(c => c.User).Where(r => r.Status == RentalStatus.Confirmed).ToListAsync();
        }

        public async Task<IList<Rental>> GetAllRejectedRentals()
        {
            return await dbContext.Rentals.Include(r => r.User).Include(r => r.Car).ThenInclude(c => c.User).Where(r => r.Status == RentalStatus.Rejected).ToListAsync();
        }
    }
}
