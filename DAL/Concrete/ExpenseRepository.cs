using System.Linq.Expressions;
using DAL.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class ExpenseRepository : BaseRepository<Expense, ExpenseContext>, IExpenseRepository
    {
        public ExpenseRepository(ExpenseContext expenseContext) : base(expenseContext)
        {
        }

        public override List<Expense> GetAll(Expression<Func<Expense, bool>> filter = null)
        {
            return filter == null
                ? _expenseContext.Expenses.Include(e => e.Category).ToList()
                : _expenseContext.Expenses.Include(e => e.Category).Where(filter).ToList();
        }

        public List<Expense> GetByCategory(Expression<Func<Expense, bool>> filter)
        {
            return _expenseContext.Expenses
                .Include(e => e.Category)
                .Where(filter)
                .ToList();
        }

    }
}
