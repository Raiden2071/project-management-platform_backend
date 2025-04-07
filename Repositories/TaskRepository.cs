using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Data;
using TaskManager.DTOs;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TaskRepository> _logger;

        public TaskRepository(AppDbContext context, ILogger<TaskRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Models.Task>> GetAllTasksAsync()
        {
            try
            {
                return await _context.Tasks.OrderByDescending(t => t.CreatedAt).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all tasks");
                throw;
            }
        }

        public async Task<IEnumerable<Models.Task>> GetFilteredTasksAsync(TaskFilterDto filter)
        {
            try
            {
                // Начинаем с запроса всех задач
                IQueryable<Models.Task> query = _context.Tasks;
                
                // Применяем фильтры, если они указаны
                
                // Фильтрация по дате начала
                if (filter.StartDate.HasValue)
                {
                    query = query.Where(t => t.StartDate.Date == filter.StartDate.Value.Date);
                }
                
                // Фильтрация по приоритету
                if (filter.Priority.HasValue)
                {
                    query = query.Where(t => t.Priority == filter.Priority.Value);
                }
                
                // Фильтрация по статусу
                if (filter.IsCompleted.HasValue)
                {
                    query = query.Where(t => t.IsCompleted == filter.IsCompleted.Value);
                }
                
                // Поиск по названию
                if (!string.IsNullOrWhiteSpace(filter.TitleContains))
                {
                    query = query.Where(t => t.Title.Contains(filter.TitleContains));
                }
                
                // По умолчанию сортируем по дате создания (от новых к старым)
                query = query.OrderByDescending(t => t.CreatedAt);
                
                // Получаем итоговый список
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting filtered tasks");
                throw;
            }
        }

        public async Task<Models.Task?> GetTaskByIdAsync(int id)
        {
            try
            {
                return await _context.Tasks.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting task with ID {TaskId}", id);
                throw;
            }
        }

        public async Task<Models.Task> CreateTaskAsync(Models.Task task)
        {
            try
            {
                task.CreatedAt = DateTime.UtcNow;
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating task");
                throw;
            }
        }

        public async Task<Models.Task?> UpdateTaskAsync(Models.Task task)
        {
            try
            {
                var existingTask = await _context.Tasks.FindAsync(task.Id);
                if (existingTask == null)
                {
                    return null;
                }

                // Update properties
                existingTask.Title = task.Title;
                existingTask.DueDate = task.DueDate;
                existingTask.Priority = task.Priority;
                existingTask.IsCompleted = task.IsCompleted;
                existingTask.UpdatedAt = DateTime.UtcNow;

                _context.Tasks.Update(existingTask);
                await _context.SaveChangesAsync();
                return existingTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating task with ID {TaskId}", task.Id);
                throw;
            }
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);
                if (task == null)
                {
                    return false;
                }

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting task with ID {TaskId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<Models.Task>> GetTasksByPriorityAsync(TaskPriority priority)
        {
            try
            {
                return await _context.Tasks
                    .Where(t => t.Priority == priority)
                    .OrderByDescending(t => t.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting tasks by priority {Priority}", priority);
                throw;
            }
        }

        public async Task<IEnumerable<Models.Task>> GetCompletedTasksAsync(bool isCompleted)
        {
            try
            {
                return await _context.Tasks
                    .Where(t => t.IsCompleted == isCompleted)
                    .OrderByDescending(t => t.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting tasks by completion status {IsCompleted}", isCompleted);
                throw;
            }
        }
    }
} 