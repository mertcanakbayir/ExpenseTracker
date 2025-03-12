using System.Linq.Expressions;
using AutoMapper;
using Core.Business;
using Core.DAL;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class BaseService<TDto, TEntity> : IBaseService<TDto, TEntity>
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

        public void Add(TDto dto)
        {
            if (dto == null) throw new ArgumentNullException("Eklenmek istenen öğe null olamaz!");
            var entity = _mapper.Map<TEntity>(dto);
            _baseRepository.Add(entity);
        }

        public void Delete(Guid id)
        {
            var entity = _baseRepository.Get(e => EF.Property<Guid>(e, "Id") == id);
            if (entity == null) throw new KeyNotFoundException("Silinecek nesne bulunamadı.");
            _baseRepository.Delete(entity);
        }

        public bool Exists(Expression<Func<TEntity, bool>> filter)
        {
            return _baseRepository.Exists(filter);
        }

        public TDto Get(Expression<Func<TEntity, bool>> filter)
        {
            var entity = _baseRepository.Get(filter);
            if (entity == null) throw new KeyNotFoundException("Aranan nesne bulunamadı.");
            return _mapper.Map<TDto>(entity);
        }

        public List<TDto> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            var entities = _baseRepository.GetAll(filter);
            return _mapper.Map<List<TDto>>(entities);
        }

        public void Update(TDto dto)
        {
            if (dto == null) throw new ArgumentNullException("Güncellenmek istenen öğe null olamaz");
            var entity = _mapper.Map<TEntity>(dto);
            _baseRepository.Update(entity);
        }
    }
}
