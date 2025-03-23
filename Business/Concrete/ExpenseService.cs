using Business.Abstract;
using Core.DTOs;
using DAL.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }
        public void Add(ExpenseDto expenseDto)
        {
            var newExpense = new Expense { 
            Title = expenseDto.Title,
            Description = expenseDto.Description,
            Amount = expenseDto.Amount,
            ExpenseDate = expenseDto.ExpenseDate,
            CategoryId = expenseDto.CategoryId,
            UserId = expenseDto.UserId,
            };

            _expenseRepository.Add(newExpense);
        }

        public void Delete(Guid id)
        {
            var expense=_expenseRepository.Get(e=>e.Id == id);
            if(expense==null)
            {
                throw new Exception("Expense Bulunamadı!");
            }

            _expenseRepository.Delete(expense);
        }

        public List<ExpenseDto> GetAll()
        {
           var expenses = _expenseRepository.GetAll();
            return expenses.Select(e => new ExpenseDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Amount = e.Amount,
                ExpenseDate = e.ExpenseDate,
                CategoryId = e.CategoryId,
                UserId = e.UserId
            }).ToList();
        }

        public List<ExpenseDto> GetByCategory(Guid categoryId)
        {
            var expenses = _expenseRepository.GetByCategory(e=>e.CategoryId==categoryId);
            return expenses.Select(e => new ExpenseDto
            {
                Id = e.Id,
                Description = e.Description,
                Amount = e.Amount,
                ExpenseDate = e.ExpenseDate,
                CategoryId = e.CategoryId,
                UserId = e.UserId
            }).ToList();
        }

        public void Update(Guid id, ExpenseDto expense)
        {
            var existingExpense=_expenseRepository.Get(e=>e.Id==id);

            if (existingExpense == null)
            {
                throw new Exception("Expense bulunamadı!");
            }
            existingExpense.Title = expense.Title;
            existingExpense.Description = expense.Description;
            existingExpense.Amount = expense.Amount;
            existingExpense.ExpenseDate = expense.ExpenseDate;
            existingExpense.CategoryId = expense.CategoryId;

            _expenseRepository.Update(existingExpense);
            
        }
    }
}
