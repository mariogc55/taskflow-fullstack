using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Services;

public class TaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<TaskItemDto>> GetAllTasksAsync()
    {
        var tasks = await _taskRepository.GetAllAsync();
        
        return tasks.Select(t => new TaskItemDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status,
            CreatedAt = t.CreatedAt,
            UserId = t.UserId
        });
    }

    public async Task<TaskItemDto> CreateTaskAsync(TaskItemDto taskDto)
    {
        var taskEntity = new TaskItem
        {
            Title = taskDto.Title,
            Description = taskDto.Description,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow,
            UserId = taskDto.UserId
        };

        await _taskRepository.AddAsync(taskEntity);
        await _taskRepository.SaveChangesAsync();

        taskDto.Id = taskEntity.Id;
        return taskDto;
    }

    public async Task DeleteTaskAsync(int id)
    {
        await _taskRepository.DeleteAsync(id);
    }

    public async Task UpdateTaskAsync(TaskItem task)
    {
        await _taskRepository.UpdateAsync(task);
    }

}