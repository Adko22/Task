using DataAccess.Models;

namespace DataAccess.Repositories.ProjectRepository
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetProjectsAsync(int page, int pageSize);
    }
}
