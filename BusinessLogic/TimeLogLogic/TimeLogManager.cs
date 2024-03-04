using DataAccess.Models;
using DataAccess.Repositories.TimeLogRepository;

namespace BusinessLogic.TimeLogLogic
{
    public class TimeLogManager : ITimeLogManager
    {
        private readonly ITimeLogRepository _timeLogRepository;

        public TimeLogManager(ITimeLogRepository timeLogRepository)
        {
            _timeLogRepository = timeLogRepository;
        }

        public async Task<IEnumerable<TimeLog>> GetTimeLogsForUserAsync(int userId, DateTime? startDate, DateTime? endDate)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user ID.");
            }

            if (startDate.HasValue && endDate.HasValue && startDate.Value > endDate.Value)
            {
                throw new ArgumentException("Start date must be before end date.");
            }

            return await _timeLogRepository.GetTimeLogsForUserAsync(userId, startDate, endDate);
        }

        public async Task<IEnumerable<AggregatedTimeLogData>> GetTopEntitiesByHoursAsync(bool isProject, int count, DateTime? startDate, DateTime? endDate)
        {
            if (count <= 0)
            {
                throw new ArgumentException("Count must be positive.");
            }

            if (startDate.HasValue && endDate.HasValue && startDate.Value > endDate.Value)
            {
                throw new ArgumentException("Start date must be before end date.");
            }

            return await _timeLogRepository.GetTopEntitiesByHoursAsync(isProject, count, startDate, endDate);
        }

        public async Task<float> GetTotalHoursForUserAsync(int userId, DateTime? startDate, DateTime? endDate)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user ID.");
            }

            if (startDate.HasValue && endDate.HasValue && startDate.Value > endDate.Value)
            {
                throw new ArgumentException("Start date must be before end date.");
            }

            return await _timeLogRepository.GetTotalHoursForUserAsync(userId, startDate, endDate);
        }
    }
}
