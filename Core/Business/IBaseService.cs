using System.Linq.Expressions;

namespace Core.Business
{
    public interface IBaseService<TDto,TEntity> 
        where TDto : class 
        where TEntity : class
    {
        void Add(TDto dto);

        void Update(TDto dto);

        void Delete(Guid id);

        List<TDto> GetAll(Expression<Func<TEntity,bool>> filter=null!);

        TDto Get(Expression<Func<TEntity,bool>> filter);

        bool Exists(Expression<Func<TEntity,bool>> filter);

         void AddEntity(TEntity entity);
    }
}
