    using System.Linq.Expressions;
using AutoMapper;
using Core.DAL;
using Microsoft.EntityFrameworkCore;

namespace Core.Business
{
    public class BaseService<TDto, TEntity> : IBaseService<TDto,TEntity>
        where TDto : class
        where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _baseRepository;
        private readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public virtual void Add(TDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Eklenecek nesne boş olamaz.");

            var entity = _mapper.Map<TEntity>(dto);

            _baseRepository.Add(entity);
        }

        public virtual void Update(TDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Güncellenecek nesne boş olamaz.");

            var entity = _mapper.Map<TEntity>(dto);
            _baseRepository.Update(entity);
        }

        public void Delete(Guid id)
        {
            var entity = _baseRepository.Get(e => EF.Property<Guid>(e, "Id") == id);
            if (entity == null)
                throw new KeyNotFoundException("Silinecek nesne bulunamadı.");
            _baseRepository.Delete(entity);
        }

        public bool Exists(Expression<Func<TEntity, bool>> filter)
        {
            return _baseRepository.Exists(filter);
        }

        public TDto Get(Expression<Func<TEntity, bool>> filter)
        {
            var entity = _baseRepository.Get(filter);
            if (entity == null)
                throw new KeyNotFoundException("Aranan nesne bulunamadı.");
            return _mapper.Map<TDto>(entity);
        }

        public List<TDto> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            var entities = _baseRepository.GetAll(filter);
            return _mapper.Map<List<TDto>>(entities);
        }

        public virtual void AddEntity(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Eklenecek nesne boş olamaz.");

            _baseRepository.Add(entity);
        }
    }
}
