using CarRental.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Services
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager);
    }
}
