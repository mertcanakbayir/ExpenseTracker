using System.Linq.Expressions;

namespace Core.Business
{
    public interface IBaseService<T> where T : class
    {
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        List<T> GetAll(Expression<Func<T,bool>> filter=null!);

        T Get(Expression<Func<T,bool>> filter);

        bool Exists(Expression<Func<T,bool>> filter);
    }
}
