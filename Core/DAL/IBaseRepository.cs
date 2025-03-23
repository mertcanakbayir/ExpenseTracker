using System.Linq.Expressions;

namespace Core.DAL
{
    public interface IBaseRepository<T> where T : class
    {
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        List<T> GetAll(
     Expression<Func<T, bool>> filter = null!,
     bool includeInactive = false
 );

        T Get(
     Expression<Func<T, bool>> filter,
     bool includeInactive = false 
 );
        bool Exists(Expression<Func<T, bool>> filter);
    }
}
