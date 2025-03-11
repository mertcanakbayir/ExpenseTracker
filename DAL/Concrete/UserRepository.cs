using DAL.Abstract;
using Entities.Concrete;

namespace DAL.Concrete
{
    public class UserRepository:BaseRepository<User,ExpenseContext>,IUserRepository
    {
        public UserRepository(ExpenseContext expenseContext):base(expenseContext) { }
    }
}
