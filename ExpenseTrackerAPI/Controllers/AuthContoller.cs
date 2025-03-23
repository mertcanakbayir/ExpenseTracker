using System.Security.Claims;
using Business.Abstract;
using Business.Concrete;
using Core.DTOs;
using Core.Security.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerMVC.Controllers.Api
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public AuthController(IAuthService authService,ITokenHelper tokenHelper,IUserService userService)
        {
            _authService = authService;
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "Geçersiz giriş bilgileri." });
            }

            var token = _authService.Login(loginDto);
            if (token == null)
            {
                return Unauthorized(new { message = "Geçersiz kullanıcı adı veya şifre." });
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,  // HTTPS zorunlu
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(2)
            };

            Response.Cookies.Append("AuthToken", token.Token, cookieOptions);

            return Ok(new { Message="Login Succesful."});
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            var result = _authService.Register(registerDto);

            if (result.Count == 1 && result[0] == "Kullanıcı Kaydı Başarılı.")
            {
                return Ok(new { Message = "Register Successful." });
            }

            return BadRequest(new { Errors = result });
        }
        [Authorize]
        [HttpGet("user-info")]
        public IActionResult GetUserInfo()
        {
            var token = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Token not found" });
            }

            var userId = _tokenHelper.ValidateToken(token);
            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            var user = _userService.Get(userId.Value);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(new { user.Id,user.Username, user.Email });
        }



        [HttpPost("logout")]
        public IActionResult Logout()
        {
            if (Request.Cookies["AuthToken"] != null)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(-1) // Çerezi hemen geçersiz kıl
                };

                Response.Cookies.Append("AuthToken", "", cookieOptions);
            }

            return Ok(new { message = "Logout successful." });
        }
    }
}
