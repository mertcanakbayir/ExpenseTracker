using System.Linq.Expressions;
using Core.DAL;
using Entities.Concrete;

namespace DAL.Abstract
{
    public interface IExpenseRepository:IBaseRepository<Expense>
    {
        List<Expense> GetByCategory(Expression<Func<Expense, bool>> filter, bool includeInactive = false);
    }
}
