using DataAccess.Context;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.TimeLogRepository
{
    public class TimeLogRepository : ITimeLogRepository
    {
        private readonly TaskContext _context;

        public TimeLogRepository(TaskContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TimeLog>> GetTimeLogsForUserAsync(int userId, DateTime? startDate, DateTime? endDate)
        {
            IQueryable<TimeLog> timeLogs = _context.TimeLogs.Where(tl => tl.UserId == userId);

            if (startDate.HasValue)
            {
                timeLogs = timeLogs.Where(tl => tl.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                timeLogs = timeLogs.Where(tl => tl.Date <= endDate.Value);
            }

            return await timeLogs.ToListAsync();
        }

        public async Task<IEnumerable<AggregatedTimeLogData>> GetTopEntitiesByHoursAsync(bool isProject, int count, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.TimeLogs.AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(tl => tl.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(tl => tl.Date <= endDate.Value);
            }
            if (isProject)
            {
                var projectTimeLogs = from tl in query
                                      join project in _context.Projects on tl.ProjectId equals project.Id
                                      group tl by new { tl.ProjectId, project.Name } into grouped
                                      select new AggregatedTimeLogData
                                      {
                                          EntityId = grouped.Key.ProjectId,
                                          EntityName = grouped.Key.Name,
                                          TotalHours = (float)grouped.Sum(g => g.Hours)
                                      };

                return await projectTimeLogs.OrderByDescending(x => x.TotalHours).Take(count).ToListAsync();
            }
            else
            {
                var userTimeLogs = from tl in query
                                   join user in _context.Users on tl.UserId equals user.Id
                                   group new { tl, user } by new { tl.UserId, user.FirstName, user.LastName } into grouped
                                   select new AggregatedTimeLogData
                                   {
                                       EntityId = grouped.Key.UserId,
                                       EntityName = grouped.Key.FirstName + " " + grouped.Key.LastName,
                                       TotalHours = (float)grouped.Sum(g => g.tl.Hours)
                                   };

                return await userTimeLogs.OrderByDescending(x => x.TotalHours).Take(count).ToListAsync();
            }
        }


        public async Task<float> GetTotalHoursForUserAsync(int userId, DateTime? startDate, DateTime? endDate)
        {
            IQueryable<TimeLog> query = _context.TimeLogs.Where(tl => tl.UserId == userId);

            if (startDate.HasValue)
            {
                query = query.Where(tl => tl.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(tl => tl.Date <= endDate.Value);
            }

            return (float)await query.SumAsync(tl => tl.Hours);
        }
    }
}
