using DataAccess.Context;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.ProjectRepository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly TaskContext _context;

        public ProjectRepository(TaskContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync(int page, int pageSize)
        {
            IQueryable<Project> projects = _context.Projects;

            projects = projects.Skip((page - 1) * pageSize).Take(pageSize);

            return await projects.ToListAsync();
        }
    }
}
