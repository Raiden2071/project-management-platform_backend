using System;
using System.ComponentModel.DataAnnotations;
using TaskManager.Models;

namespace TaskManager.DTOs
{
    // DTO for creating a new task
    public class CreateTaskDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        
        public DateTime StartDate { get; set; } = DateTime.Today;

        public DateTime? DueDate { get; set; }
        
        public TaskPriority Priority { get; set; } = TaskPriority.Normal;
    }
    
    // DTO for updating a task
    public class UpdateTaskDto
    {
        [StringLength(100)]
        public string? Title { get; set; }

        public DateTime? StartDate { get; set; }
        
        public DateTime? DueDate { get; set; }
        
        public TaskPriority? Priority { get; set; }
        
        public bool? IsCompleted { get; set; }
    }
    
    // DTO for returning a task in responses
    public class TaskDto
    {
        public int Id { get; set; }
        
        public string Title { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        
        public DateTime? DueDate { get; set; }
        
        public TaskPriority Priority { get; set; }
        
        public bool IsCompleted { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
    }
} 