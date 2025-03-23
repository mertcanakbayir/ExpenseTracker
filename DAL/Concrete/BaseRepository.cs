using System.Linq.Expressions;
using Core.DAL;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity> where TEntity : class
        where TContext : DbContext
    {
        protected readonly ExpenseContext _expenseContext;

        public BaseRepository(ExpenseContext expenseContlext)
        {
            _expenseContext = expenseContlext;
        }
        public void Add(TEntity entity)
        {
           var addedEntity=_expenseContext.Entry(entity);
            addedEntity.State = EntityState.Added;
            _expenseContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.IsActive = false; 
                _expenseContext.Update(entity);
                _expenseContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("TEntity must inherit from BaseEntity");
            }
        }


        public bool Exists(Expression<Func<TEntity, bool>> filter)
        {
            return _expenseContext.Set<TEntity>().Any(filter);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter, bool includeInactive = false)
        {
            var query = _expenseContext.Set<TEntity>().AsQueryable();

            // Soft delete kontrolü sadece BaseEntity'den türeyenler ve includeInactive false ise
            if (!includeInactive && typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
            {
                query = query.Where(e => ((BaseEntity)(object)e).IsActive);
            }

            return query.FirstOrDefault(filter);
        }

        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, bool includeInactive = false)
        {
            var query = _expenseContext.Set<TEntity>().AsQueryable();

            if (!includeInactive && typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
            {
                query = query.Where(e => ((BaseEntity)(object)e).IsActive);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }


        public void Update(TEntity entity)
        {
            var entry = _expenseContext.Entry(entity);
            var id = entry.Property("Id").CurrentValue; 

            var existingEntity = _expenseContext.Set<TEntity>().Find(id);

            if (existingEntity == null)
                throw new KeyNotFoundException("Güncellenecek nesne veritabanında bulunamadı.");

            _expenseContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            _expenseContext.SaveChanges();
        }
    }
}
