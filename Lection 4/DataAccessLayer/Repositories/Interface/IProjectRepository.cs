using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interface
{
    public interface IProjectRepository : IRepository<Project>
    {
        // №2 Хотя бы придумать как сделать постраничный вывод(В идеале с фильтрами по имени, категорией и диапазону дат(А еще в идеале реализовать это))
        public Task<Project> GetProjectByIdWithAllInfoAsync(Guid Id);

    }
}
