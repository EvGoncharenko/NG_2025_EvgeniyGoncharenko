using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<ICollection<T>> GetAllAsync();

        Task<ICollection<T>> GetAllWithPageAsync(int pageNumber, int pageSize);
        Task<T?> GetAsyncById(Guid id);

        Task<Guid> CreateAsync(T entity);
        Task<Guid> UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
