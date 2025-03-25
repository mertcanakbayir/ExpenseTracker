using Core.DTOs;

namespace Business.Abstract
{
    public interface IExpenseService
    {
        void Add(ExpenseDto expense);

        void Update(Guid id, ExpenseDto expense);

        void Delete(Guid id);

        List<ExpenseDto> GetAll();

        List<ExpenseDto> GetByCategory(Guid id);
       
        ExpenseDto Get(Guid id);

    }
}
