using System;
using TaskManager.Models;

namespace TaskManager.DTOs
{
    public class TaskFilterDto
    {
        public DateTime? StartDate { get; set; }

        public TaskPriority? Priority { get; set; }

        public bool? IsCompleted { get; set; }

        public string? TitleContains { get; set; }
    }
}
