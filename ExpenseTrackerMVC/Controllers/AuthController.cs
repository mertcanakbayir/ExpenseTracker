using Business.Abstract;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto) {
            try
            {
                var result = _authService.Login(loginDto);
                if (result == "Giriş yapıldı.")
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
      
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto registerDto) {

            try
            {
                var result = _authService.Register(registerDto);
                if (result == "Kullanıcı Kaydı Başarılı.")
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

       

        }
    }
}
