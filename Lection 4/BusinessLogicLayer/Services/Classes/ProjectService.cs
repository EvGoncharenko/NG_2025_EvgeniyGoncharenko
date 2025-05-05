using AutoMapper;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services.Interface;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interface;


namespace BusinessLogicLayer.Services.Classes
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }



        public async Task<Guid> CreateAsync(ProjectModel model)
        {
            var project = _mapper.Map<Project>(model);
            return await _projectRepository.CreateAsync(project);
        }

        public async Task<List<ProjectModel>> GetAllProject()
        {
            var allProject = await _projectRepository.GetAllAsync();
            return _mapper.Map<List<ProjectModel>>(allProject);
        }

        public async Task<GetProjectModel> GetByIdAsync (Guid id)
        {
            var project = await _projectRepository.GetProjectByIdWithAllInfoAsync (id);

            var model = _mapper.Map<GetProjectModel>(project);
            model.VoteCaount = project.Vote.Count;
            return model;
        }

        public async Task<List<ProjectModel>> GetAllProjectWithPageAsync(int pageNumber, int pageSize)
        {
            var project = await _projectRepository.GetAllWithPageAsync (pageNumber, pageSize);
            return _mapper.Map<List<ProjectModel>> (project);
        }
    }
}
