using DataAccess.Models;

namespace DataAccess.Repositories.TimeLogRepository
{
    public interface ITimeLogRepository
    {
        Task<IEnumerable<TimeLog>> GetTimeLogsForUserAsync(int userId, DateTime? startDate, DateTime? endDate);

        Task<IEnumerable<AggregatedTimeLogData>> GetTopEntitiesByHoursAsync(bool isProject, int count, DateTime? startDate, DateTime? endDate);

        Task<float> GetTotalHoursForUserAsync(int userId, DateTime? startDate, DateTime? endDate);
    }
}

