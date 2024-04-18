using CarRental.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Repositories
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<IReadOnlyList<Review>> GetAllReviwsForCar(int carId);
    }
}
