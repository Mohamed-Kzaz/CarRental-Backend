using CarRental.Core;
using CarRental.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICarRepository CarRepository { get; set; }

        public UnitOfWork(ICarRepository carRepository)
        {
            CarRepository = carRepository;
        }

    }
}
