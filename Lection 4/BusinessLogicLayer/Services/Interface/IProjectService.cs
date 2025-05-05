using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services.Interface
{
    public interface IProjectService 
    {
        Task<Guid> CreateAsync(ProjectModel model);

        Task<List<ProjectModel>> GetAllProject();

        Task<List<ProjectModel>> GetAllProjectWithPageAsync(int pageNumber, int pageSize);
        Task<GetProjectModel> GetByIdAsync(Guid id);


    }
}
