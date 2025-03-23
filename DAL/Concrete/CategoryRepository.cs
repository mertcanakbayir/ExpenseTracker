using DAL.Abstract;
using Entities.Concrete;

namespace DAL.Concrete
{
    public class CategoryRepository : BaseRepository<Category, ExpenseContext>, ICategoryRepository
    {
        public CategoryRepository(ExpenseContext expenseContext) : base(expenseContext)
        {
        }

    }
}
