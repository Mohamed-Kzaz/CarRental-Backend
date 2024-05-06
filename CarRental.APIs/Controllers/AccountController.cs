using CarRental.APIs.DTOs.Account;
using CarRental.APIs.DTOs.Rental;
using CarRental.APIs.Helper;
using CarRental.Core.Entities;
using CarRental.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace CarRental.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromForm] RegisterDto model)
        {
            if (CheckEmailExists(model.Email).Result.Value)
                return BadRequest(new { Message = "Email is already exist" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var drivingLic = DocumentSettings.UploadFile(model.DrivingLic, "images");

            var imageProfile = DocumentSettings.UploadFile(model.ImageProfile, "images");

            var nationalId = DocumentSettings.UploadFile(model.NationalIdImage, "images");

            var user = new ApplicationUser()
            {
                ImageProfileURl = imageProfile,
                FName = model.FName,
                LName = model.LName,
                Email = model.Email,
                Address = model.Address,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
                DOB = model.DOB,
                DrivingLicURl = drivingLic,
                NationalIdURl = nationalId,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(new { Message = "Something wrong happened when register!" });

            await _userManager.AddToRoleAsync(user, "User");

            return Ok(new { message = "success" });
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return Unauthorized(new { Message = "Email is not exist" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
                return Unauthorized(new { Message = "Password is not Valid!!" });

            var userRoles = await _userManager.GetRolesAsync(user);

            return Ok(new UserDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = string.Join(",", userRoles),
                Token = await _tokenService.CreateTokenAsync(user, _userManager),
                Message = "success"
            });
        }

        [Authorize]
        [HttpPost("changepassword")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound(new { Message = "User is not exist" });

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (changePasswordResult.Succeeded)
                return Ok(new { Message = "Password changed successfully" });

            return BadRequest(ModelState);
        }

        [HttpGet("get-all-users")]
        public async Task<ActionResult<UserToReturnDto>> GetAllUsers()
        {
            var userRole = await _roleManager.FindByNameAsync("User");

            if (userRole is null)
                return NotFound(new { message = "There are no users" });

            var users = await _userManager.GetUsersInRoleAsync(userRole.Name);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";

            var mappedUsers = new List<UserToReturnDto>();

            foreach (var user in users)
            {
                var mappedUser = new UserToReturnDto
                {
                    Id = user.Id,
                    ProfileURl = baseUrl + user.ImageProfileURl,
                    FName = user.FName,
                    LName = user.LName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    DOB = user.DOB,
                    DrivingLicURl = baseUrl + user.DrivingLicURl,
                    NationalIdURl = baseUrl + user.NationalIdURl
                };

                mappedUsers.Add(mappedUser);
            }
            return Ok(mappedUsers);
        }
        [HttpDelete("deleteUser")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { Message = "User is not exist" });
            }

            DocumentSettings.DeleteFile(user.DrivingLicURl, "images");

            await _userManager.DeleteAsync(user);

            return Ok(new { Message = "User deleted successfully" });
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
    }
}
