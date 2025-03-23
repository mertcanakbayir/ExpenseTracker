using Business.Abstract;
using Core.DTOs;
using DAL.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;
        public CategoryService(ICategoryRepository categoryRepository,ICurrentUserService currentUserService)
        {
            _categoryRepository = categoryRepository;   
            _currentUserService = currentUserService;
        }
        public void Add(CategoryDto categoryDto)
        {
            var currentUser=_currentUserService.UserId;
            var newCategory = new Category {
            CategoryName=categoryDto.CategoryName,
            UserId=currentUser
            };
            _categoryRepository.Add(newCategory);
        }
        

        public void Delete(Guid id)
        {
            var currentUser = _currentUserService.UserId;
            var category = _categoryRepository.Get(c => c.Id == id && c.UserId==currentUser);
            if (category == null) {
                throw new Exception("Category bulunamadı!");
            }
            _categoryRepository.Delete(category);
        }

        public List<CategoryDto> GetAll()
        {
            var currentUser= _currentUserService.UserId;    

            var categories=_categoryRepository.GetAll(c=>c.UserId==currentUser);
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
