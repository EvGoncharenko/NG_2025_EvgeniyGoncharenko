using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryModel>> GetAllCategory();
    }
}
