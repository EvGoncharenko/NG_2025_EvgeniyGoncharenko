using AutoMapper;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services.Interface;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interface;

namespace BusinessLogicLayer.Services.Classes
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IMapper _mapper;

        public VoteService(IVoteRepository voteRepository, IMapper mapper)
        {
            _voteRepository = voteRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddVoteAsync(VoteModel model)
        {
            var vote = _mapper.Map<Vote>(model);
            return await _voteRepository.CreateAsync(vote);
        }
    }
}
