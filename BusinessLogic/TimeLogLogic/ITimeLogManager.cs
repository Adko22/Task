using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.TimeLogLogic
{
    public interface ITimeLogManager
    {
        Task<IEnumerable<TimeLog>> GetTimeLogsForUserAsync(int userId, DateTime? startDate, DateTime? endDate);

        Task<IEnumerable<AggregatedTimeLogData>> GetTopEntitiesByHoursAsync(bool isProject, int count, DateTime? startDate, DateTime? endDate);

        Task<float> GetTotalHoursForUserAsync(int userId, DateTime? startDate, DateTime? endDate);
    }
}
