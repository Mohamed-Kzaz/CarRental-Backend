using CarRental.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Repository.Data
{
    public class CarRentalContext : IdentityDbContext<ApplicationUser>
    {
        public CarRentalContext(DbContextOptions<CarRentalContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Rental>().Property(p => p.Total_Cost).HasColumnType("decimal(18,2)");
        }


        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
