using System.Linq.Expressions;
using Core.Business;
using Core.DAL;

namespace Business.Concrete
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _baseRepository;
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public void Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Eklenmek istenen öğe null olamaz!");

            _baseRepository.Add(entity);
        }

        public void Delete(T entity)
        {

            if (entity == null) throw new ArgumentNullException("Silinmek istenen öğe null olamaz");

            if (!Exists(e => e == entity))
                throw new KeyNotFoundException("Silinecek nesne bulunamadı.");
            _baseRepository.Delete(entity);
        }

        public bool Exists(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                throw new KeyNotFoundException("Nesne bulunamadı");
            }
            return _baseRepository.Exists(filter);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            if (filter == null) throw new KeyNotFoundException("Aranan nesne bulunamadı.");
            return _baseRepository.Get(filter);
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            return _baseRepository.GetAll(filter).ToList();

        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Güncellenmek istenen öğe null olamaz");
            if (!Exists(e => e == entity))
                throw new KeyNotFoundException("Güncellenecek nesne bulunamadı.");
            _baseRepository.Update(entity);
        }
    }
}
