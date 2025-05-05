using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services.Interface
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUserAsync();
    }
}
