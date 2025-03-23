using Business.Abstract;
using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerMVC.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    { 
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(Guid id)
        {
            var user = _userService.Get(id);
            if (user == null)
                return NotFound("Kullanıcı bulunamadı.");

            return Ok(user);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] UserDto userDto)
        {
            try
            {
                _userService.Add(userDto);
                return Ok("Kullanıcı başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UserDto userDto)
        {
            try
            {
                _userService.Update(id, userDto);
                return Ok("Kullanıcı başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
