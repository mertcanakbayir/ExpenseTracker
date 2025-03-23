using Core.DTOs;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        void Add(CategoryDto category);

        void Update(Guid id, CategoryDto category);

        void Delete(Guid id);

        List<CategoryDto> GetAll();
    }
}
