
using DataAccess.Models;

namespace BusinessLogic.ProjectLogic
{
    public interface IProjectManager
    {
        Task<IEnumerable<Project>> GetProjectsAsync(int page, int pageSize);
    }
}
