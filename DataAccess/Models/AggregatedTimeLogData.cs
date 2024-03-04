using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class AggregatedTimeLogData
    {
        public int EntityId { get; set; }

        public string EntityName { get; set; } = string.Empty;

        public float TotalHours { get; set; }
    }
}
