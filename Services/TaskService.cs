using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TaskManager.DTOs;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllTasksAsync();
            return tasks.Select(t => MapToTaskDto(t));
        }

        public async Task<TaskDto?> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            return task != null ? MapToTaskDto(task) : null;
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto)
        {
            var task = new Models.Task
            {
                Title = createTaskDto.Title,
                DueDate = createTaskDto.DueDate,
                Priority = createTaskDto.Priority,
                StartDate = createTaskDto.StartDate,
                CreatedAt = DateTime.UtcNow
            };

            var createdTask = await _taskRepository.CreateTaskAsync(task);
            return MapToTaskDto(createdTask);
        }

        public async Task<TaskDto?> UpdateTaskAsync(int id, UpdateTaskDto updateTaskDto)
        {
            var existingTask = await _taskRepository.GetTaskByIdAsync(id);
            if (existingTask == null)
            {
                return null;
            }

            // Update only the properties that were provided
            if (updateTaskDto.Title != null)
            {
                existingTask.Title = updateTaskDto.Title;
            }

            if (updateTaskDto.StartDate.HasValue)
            {
                existingTask.StartDate = updateTaskDto.StartDate.Value;
            }
            
            if (updateTaskDto.DueDate.HasValue)
            {
                existingTask.DueDate = updateTaskDto.DueDate;
            }
            
            if (updateTaskDto.Priority.HasValue)
            {
                existingTask.Priority = updateTaskDto.Priority.Value;
            }
            
            if (updateTaskDto.IsCompleted.HasValue)
            {
                existingTask.IsCompleted = updateTaskDto.IsCompleted.Value;
            }

            var updatedTask = await _taskRepository.UpdateTaskAsync(existingTask);
            return updatedTask != null ? MapToTaskDto(updatedTask) : null;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await _taskRepository.DeleteTaskAsync(id);
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByPriorityAsync(TaskPriority priority)
        {
            var tasks = await _taskRepository.GetTasksByPriorityAsync(priority);
            return tasks.Select(t => MapToTaskDto(t));
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByCompletionStatusAsync(bool isCompleted)
        {
            var tasks = await _taskRepository.GetCompletedTasksAsync(isCompleted);
            return tasks.Select(t => MapToTaskDto(t));
        }

        // Helper method to map from Task entity to TaskDto
        private static TaskDto MapToTaskDto(Models.Task task)
        {
            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                StartDate = task.StartDate,
                DueDate = task.DueDate,
                Priority = task.Priority,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            };
        }
    }
} 