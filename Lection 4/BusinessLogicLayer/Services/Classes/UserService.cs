using AutoMapper;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services.Interface;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interface;

namespace BusinessLogicLayer.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly IUserReposytory _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserReposytory repository, IMapper mapper)
        {
            _userRepository = repository;
            _mapper = mapper;
        }

        public async Task<List<UserModel>> GetAllUserAsync()
        {
            var allUser = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserModel>>(allUser);
        }
    }
}
