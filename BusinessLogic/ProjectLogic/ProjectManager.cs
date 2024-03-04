using DataAccess.Models;
using DataAccess.Repositories.ProjectRepository;

namespace BusinessLogic.ProjectLogic
{
    public class ProjectManager : IProjectManager
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectManager(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync(int page, int pageSize)
        {
            if (page <= 0)
            {
                throw new ArgumentException("Page number must be greater than zero.", nameof(page));
            }

            if (pageSize <= 0)
            {
                throw new ArgumentException("Page size must be greater than zero.", nameof(pageSize));
            }

            return await _projectRepository.GetProjectsAsync(page, pageSize);
        }
    }
}
