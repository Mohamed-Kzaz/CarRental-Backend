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
    }
}
