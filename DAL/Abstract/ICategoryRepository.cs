using Core.DAL;
using DAL.Concrete;
using Entities.Concrete;

namespace DAL.Abstract
{
    public interface ICategoryRepository:IBaseRepository<Category>
    {
    }
}
