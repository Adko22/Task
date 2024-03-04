﻿#nullable disable

namespace DataAccess.Models;

public class User
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public virtual ICollection<TimeLog> TimeLogs { get; set; } = new List<TimeLog>();
}