using BlogApp.Backend.DTOs.Account;
using BlogApp.Backend.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Backend.Controllers
{
    [Route("api/account")]

    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager; 
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var userExists = await _userManager.FindByEmailAsync(userDto.Email);

                if (userExists != null) return BadRequest("E-Mail is already registered use a different email");

                var appUser = new AppUser()
                {
                    Email = userDto.Email,
                    UserName = userDto.UserName
                };

                var result = await _userManager.CreateAsync(appUser, userDto.Password);

                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok("User Created");
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                return StatusCode(500, result.Errors);


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid) return BadRequest("Please enter the required information");
            var user = await _userManager.FindByEmailAsync(loginUserDto.Email);
            if (user == null) return BadRequest("user doesnot exists");

            var signInRes = await _signInManager.PasswordSignInAsync(user, loginUserDto.Password, false, false);
            if (signInRes.Succeeded)
            {
                var response = new { UserName = user.UserName, Email = user.Email };
                return Ok(response);
            }
            return BadRequest("Signin failed");
        }
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
                return Ok();
            }
            else
            {
                return BadRequest("you are already logged out");
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser(DeleteUserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userExists = await _userManager.FindByEmailAsync(userDto.Email);

            if (userExists == null) return BadRequest("Incorrect Email");



            var signinResult = await _signInManager.CheckPasswordSignInAsync(userExists, userDto.Password, true);

            if (!signinResult.Succeeded)
            {
                return Unauthorized("Incorrect password");
            }

            var deleteResult = await _userManager.DeleteAsync(userExists);
            if (deleteResult.Succeeded) return Ok("User deleted");
            return BadRequest("something went wrong");

        }
    }
}
