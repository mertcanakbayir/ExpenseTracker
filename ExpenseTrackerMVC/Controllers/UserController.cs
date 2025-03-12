using Business.Abstract;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Add([FromBody] UserDto userDto)
        {
            try
            {
                _userService.Add(userDto);
                return Ok("Kullanıcı başarıyla eklendi.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);  // BaseService'de kontrol edildiği için buraya düşer.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }

        }

        [HttpGet("get/{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var user = _userService.Get(u => u.Id == id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                var users = _userService.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] UserDto userDto)
        {
            try
            {
                _userService.Update(userDto);
                return Ok("Kullanıcı başarıyla güncellendi.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _userService.Delete(id);
                return Ok("Kullanıcı başarıyla silindi.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

    }
}
