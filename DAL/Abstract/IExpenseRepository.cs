using System.Linq.Expressions;
using Core.DAL;
using Entities.Concrete;

namespace DAL.Abstract
{
    public interface IExpenseRepository:IBaseRepository<Expense>
    {
        List<Expense> GetByCategory(Expression<Func<Expense, bool>> filter, bool includeInactive = false);

        List<Expense> GetFiltered(
             Guid? categoryId = null,
             int? year = null,
             int? month = null,
             bool includeInactive = false
         );

    }
}
