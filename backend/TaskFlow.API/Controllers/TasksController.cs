using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Services;
using TaskFlow.Domain.Entities;

namespace TaskFlow.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;

    public TasksController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetTasks()
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

        int userId = int.Parse(userIdString);
        
        var tasks = await _taskService.GetTasksByUserIdAsync(userId);
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<ActionResult<TaskItemDto>> CreateTask([FromBody] TaskItemDto taskDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

        taskDto.UserId = int.Parse(userIdString);

        var createdTask = await _taskService.CreateTaskAsync(taskDto);
        return CreatedAtAction(nameof(GetTasks), new { id = createdTask.Id }, createdTask);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        try
        {
            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItem task)
    {
        if (id != task.Id)
        {
            return BadRequest();
        }

        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

        task.UserId = int.Parse(userIdString);

        try
        {
            await _taskService.UpdateTaskAsync(task);
            return NoContent();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}