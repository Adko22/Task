#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public class Project
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<TimeLog> TimeLogs { get; set; } = new List<TimeLog>();
}