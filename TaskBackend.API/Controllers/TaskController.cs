
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBackend.Domain.DTOs;
using TaskBackend.Domain.Entities;
using TaskBackend.Domain.Enums;
using TaskBackend.Infrastucture.Data;

namespace TaskBackend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskDbContext _context;

    public TasksController(TaskDbContext context)
    {
        _context = context;
    }

    // GET: api/tasks
    [HttpGet]
    public async Task<IActionResult> GetTasks(
        [FromQuery] TaskStates? states,
        [FromQuery] string? responsible,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        if (page < 1 || pageSize < 1 || pageSize > 100)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Paginación inválida",
                Detail = "page debe ser mayor que 0 y pageSize debe estar entre 1 y 100.",
                Status = 400
            });
        }

        var query = _context.Tasks.AsNoTracking().AsQueryable();

        // Filtrar por estado
        if (states.HasValue)
        {
            query = query.Where(t => t.States == states.Value);
        }

        // Filtrar por responsable
        if (!string.IsNullOrWhiteSpace(responsible))
        {
            var name = responsible.Trim().ToLower();

            query = query.Where(t =>
                t.Responsible.ToLower() == name);
        }

        var totalItems = await query.CountAsync();

        var tasks = await query
            .OrderBy(t => t.DueDate)
            .ThenBy(t => t.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                States = t.States,
                Responsible = t.Responsible,
                DueDate = t.DueDate,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            })
            .ToListAsync();

        return Ok(new
        {
            Items = tasks,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(
                totalItems / (double)pageSize)
        });
    }

    // GET: api/tasks/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var task = await _context.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Tarea no encontrada",
                Status = 404
            });
        }

        return Ok(new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            States = task.States,
            Responsible = task.Responsible,
            DueDate = task.DueDate,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt
        });
    }

    // POST: api/tasks
    [HttpPost]
    public async Task<IActionResult> CreateTask(
        CreateTaskDto dto)
    {
        var now = DateTime.UtcNow;

        var task = new TaskItem
        {
            Title = dto.Title.Trim(),
            Description = dto.Description.Trim(),
            States = dto.States,
            Responsible = dto.Responsible.Trim(),
            DueDate = dto.DueDate!.Value,
            CreatedAt = now,
            UpdatedAt = now
        };

        _context.Tasks.Add(task);

        await _context.SaveChangesAsync();

        var response = new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            States = task.States,
            Responsible = task.Responsible,
            DueDate = task.DueDate,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt
        };

        return CreatedAtAction(
            nameof(GetTask),
            new { id = task.Id },
            response);
    }

    // PUT: api/tasks/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto dto)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Tarea no encontrada",
                Status = 404
            });
        }

        task.Title = dto.Title.Trim();
        task.Description = dto.Description.Trim();
        task.States = dto.States;
        task.Responsible = dto.Responsible.Trim();
        task.DueDate = dto.DueDate!.Value;
        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            States = task.States,
            Responsible = task.Responsible,
            DueDate = task.DueDate,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt
        });
    }

    // DELETE: api/tasks/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Tarea no encontrada",
                Status = 404
            });
        }

        _context.Tasks.Remove(task);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
