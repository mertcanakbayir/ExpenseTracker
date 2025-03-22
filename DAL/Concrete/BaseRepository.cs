using System.Linq.Expressions;
using Core.DAL;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity> where TEntity : class
        where TContext : DbContext
    {
        protected readonly ExpenseContext _expenseContext;

        public BaseRepository(ExpenseContext expenseContext)
        {
            _expenseContext = expenseContext;
        }
        public void Add(TEntity entity)
        {
           var addedEntity=_expenseContext.Entry(entity);
            addedEntity.State = EntityState.Added;
            _expenseContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            var deletedEntity=_expenseContext.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            _expenseContext.SaveChanges();
        }

        public bool Exists(Expression<Func<TEntity, bool>> filter)
        {
            return _expenseContext.Set<TEntity>().Any(filter);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return _expenseContext.Set<TEntity>().Where(filter).FirstOrDefault();
        }

        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return _expenseContext.Set<TEntity>().ToList();
        }

        public void Update(TEntity entity)
        {
            var existingEntity = _expenseContext.Set<TEntity>().Find(EF.Property<Guid>(entity, "Id"));

            if (existingEntity == null)
                throw new KeyNotFoundException("Güncellenecek nesne veritabanında bulunamadı.");

            _expenseContext.Entry(existingEntity).CurrentValues.SetValues(entity);

            _expenseContext.SaveChanges();
        }
    }
}
