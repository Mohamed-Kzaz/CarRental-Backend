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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly CarRentalContext dbContext;
        public GenericRepository(CarRentalContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
          => await dbContext.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id)
            => await dbContext.Set<T>().FindAsync(id);

        public async Task<int> Add(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            return await dbContext.SaveChangesAsync();
        }

        public async void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            return await dbContext.SaveChangesAsync();
        }
    }
}
