using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services.Interface
{
    public interface IVoteService
    {
        Task<Guid> AddVoteAsync(VoteModel model);
    }
}
