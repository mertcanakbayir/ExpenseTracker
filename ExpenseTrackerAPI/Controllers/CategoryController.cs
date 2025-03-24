using Business.Abstract;
using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult GetAll() {
            try
            {
                var categories = _categoryService.GetAll();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpPost("add")]
        public IActionResult Add(CategoryDto categoryDto)
            {
            try
            {
                if (string.IsNullOrWhiteSpace(categoryDto.CategoryName)) {
                    return BadRequest("Kategori boş olamaz!");
                }
                _categoryService.Add(categoryDto);
                return Ok("Category başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] CategoryDto categoryDto)
        {
            try
            {
                _categoryService.Update(id, categoryDto);
                return Ok("Category Bilgileri başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _categoryService.Delete(id);
                return Ok("Category başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });

            }

        }
    }
}
