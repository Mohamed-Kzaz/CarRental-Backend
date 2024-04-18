using CarRental.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Repository.Data
{
    public class CarRentalContext : IdentityDbContext<ApplicationUser>
    {
        public CarRentalContext(DbContextOptions<CarRentalContext> options) : base(options)
        {
        }

      

        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
