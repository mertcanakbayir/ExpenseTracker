﻿using Business.Abstract;
using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            try
            {
                var expenses = _expenseService.GetAll();
                return Ok(expenses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id) {
        var expense=_expenseService.GetByCategory(id);

            try
            {
                if (expense == null)
                {
                    return NotFound("Expense bulunamadı.");
                }
                return Ok(expense);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }        
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetById(Guid id)
        {
            var expense = _expenseService.Get(id);
            try
            {
                if(expense==null)
                {
                    return NotFound("Expense bulunamadı.");
                }
                return Ok(expense);
            }
            catch (Exception ex)
            {
                 return BadRequest(ex.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult Add(ExpenseDto expenseDto)
        {
            try
            {
                _expenseService.Add(expenseDto);
                return Ok("Expense başarıyla eklendi.");
                  
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] Guid id,[FromBody]ExpenseDto expenseDto) {
            try
            {
                _expenseService.Update(id, expenseDto);
                return Ok("Expense Bilgileri başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id) {
            try
            {
                _expenseService.Delete(id);
                return Ok("Expense başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpGet("[action]")]
        public IActionResult GetFilteredExpenses([FromQuery] Guid? categoryId, [FromQuery] int? year, [FromQuery] int? month)
        {
            try
            {
               var expenses=_expenseService.GetFiltered(categoryId, year, month);
                return Ok(expenses);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(new { Error = ex.ParamName, ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.ParamName, ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "InternalServerError", ex.Message });
            }
        }
    }
}
