using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager.DTOs;

namespace TaskManager.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Models.Task>> GetAllTasksAsync();
        Task<IEnumerable<Models.Task>> GetFilteredTasksAsync(TaskFilterDto filter);
        Task<Models.Task?> GetTaskByIdAsync(int id);
        Task<Models.Task> CreateTaskAsync(Models.Task task);
        Task<Models.Task?> UpdateTaskAsync(Models.Task task);
        Task<bool> DeleteTaskAsync(int id);
        
        // Additional methods for filtering
        Task<IEnumerable<Models.Task>> GetTasksByPriorityAsync(TaskPriority priority);
        Task<IEnumerable<Models.Task>> GetCompletedTasksAsync(bool isCompleted);
    }
} 