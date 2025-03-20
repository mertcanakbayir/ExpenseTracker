using Business.Abstract;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerMVC.Controllers.Api
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
            if (result == "Kullanıcı Kaydı Başarılı.")
            {
                return Ok(new {Message="Register Succesful." });
            }

            return BadRequest(new { error = "Kayıt başarısız." });
        }
    }
}
