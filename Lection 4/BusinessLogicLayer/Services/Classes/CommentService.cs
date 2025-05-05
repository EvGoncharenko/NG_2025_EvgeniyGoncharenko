using AutoMapper;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services.Interface;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interface;

namespace BusinessLogicLayer.Services.Classes
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateCommentAsync(CommentModel model)
        {
            var comment = _mapper.Map<Comment>(model);
            return await _commentRepository.CreateAsync(comment);
        }
    }
}
