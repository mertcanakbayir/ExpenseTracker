using Business.Abstract;
using Core.DTOs;
using DAL.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;   
        }
        public void Add(CategoryDto categoryDto)
        {
            var newCategory = new Category {
                
            CategoryName=categoryDto.CategoryName
            };
            _categoryRepository.Add(newCategory);
        }
        

        public void Delete(Guid id)
        {
            var category = _categoryRepository.Get(c => c.Id == id);
            if (category == null) {
                throw new Exception("Category bulunamadı!");
            }
            _categoryRepository.Delete(category);
        }

        public List<CategoryDto> GetAll()
        {
            var categories=_categoryRepository.GetAll();
            return categories.Select(e=> new CategoryDto
            {
                Id=e.Id,
                CategoryName=e.CategoryName,
            }).ToList();
        }
        public void Update(Guid id, CategoryDto categoryDto)
        {
            var existingExpense = _categoryRepository.Get(e => e.Id == id);

            if (existingExpense == null) {
                throw new Exception("Expense bulunamadı!");
            }

            existingExpense.CategoryName = categoryDto.CategoryName;
            _categoryRepository.Update(existingExpense);
        }
    }
}
