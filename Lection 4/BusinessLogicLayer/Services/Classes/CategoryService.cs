using AutoMapper;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services.Interface;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interface;

namespace BusinessLogicLayer.Services.Classes
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryModel>> GetAllCategory()
        {
            var allCategory = await _categoryRepository.GetAllAsync();
            return _mapper.Map<List<CategoryModel>>(allCategory);
        }
    }
}
