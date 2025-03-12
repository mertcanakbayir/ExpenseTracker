using System.Linq.Expressions;

namespace Core.Business
{
    public interface IBaseService<TDto,TEntity> 
        where TDto : class 
        where TEntity : class
    {
        void Add(TDto entity);

        void Update(TDto entity);

        void Delete(Guid id);

        List<TDto> GetAll(Expression<Func<TEntity,bool>> filter=null!);

        TDto Get(Expression<Func<TEntity,bool>> filter);

        bool Exists(Expression<Func<TEntity,bool>> filter);
    }
}
