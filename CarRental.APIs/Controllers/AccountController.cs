using CarRental.APIs.DTOs.Account;
using CarRental.APIs.Helper;
using CarRental.Core.Entities;
using CarRental.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarRental.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto model)
        {
            if (CheckEmailExists(model.Email).Result.Value)
                return BadRequest(new { Message = "Email is already exist" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var fileName = DocumentSettings.UploadFile(model.DrivingLic, "images");

            var user = new ApplicationUser()
            {
                FName = model.FName,
                LName = model.LName,
                Email = model.Email,
                Address = model.Address,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
                DOB = model.DOB,
                DrivingLicURl = fileName,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(new { Message = "Something wrong happened when register!!" });

            return Ok(new { message = "you are registered successfully" });
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return Unauthorized(new {Message = "Email is not exist"});

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
                return Unauthorized(new { Message = "Password is not Valid!!" });

            return Ok(new UserDto()
            {
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager),
                Message = "you are logined successfully"
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

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
    }
}
