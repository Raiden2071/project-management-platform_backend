using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        
        public DateTime? DueDate { get; set; }
        
        public TaskPriority Priority { get; set; } = TaskPriority.Normal;
        
        public bool IsCompleted { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
    }
    
    public enum TaskPriority
    {
        Low,
        Normal,
        High,
        Urgent
    }
} 