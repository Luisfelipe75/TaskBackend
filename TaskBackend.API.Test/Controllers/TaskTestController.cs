
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBackend.API.Controllers;
using TaskBackend.Domain.DTOs;
using TaskBackend.Domain.Entities;
using TaskBackend.Domain.Enums;
using TaskBackend.Infrastucture.Data;
using Xunit;

namespace TaskBackend.API.Tests.Controllers;

public class TasksTestController
{
    private TaskDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<TaskDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new TaskDbContext(options);
    }

    [Fact]
    public async Task CreateTask_Returns201()
    {
        using var context = CreateContext();
        var controller = new TasksController(context);

        var dto = new CreateTaskDto
        {
            Title = "Prueba",
            Description = "Descripción de prueba",
            States = TaskStates.Pendiente,
            Responsible = "Ana",
            DueDate = DateTime.UtcNow.AddDays(5)
        };

        var result = await controller.CreateTask(dto);

        Assert.IsType<CreatedAtActionResult>(result);
        Assert.Single(context.Tasks);
    }

    [Fact]
    public async Task GetTask_NotFound_Returns404()
    {
        using var context = CreateContext();
        var controller = new TasksController(context);

        var result = await controller.GetTask(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task DeleteTask_ExistingTask_Returns204()
    {
        using var context = CreateContext();

        context.Tasks.Add(new TaskItem
        {
            Title = "Eliminar",
            Description = "Tarea de prueba",
            States = TaskStates.Pendiente,
            Responsible = "Carlos",
            DueDate = DateTime.UtcNow.AddDays(2)
        });

        await context.SaveChangesAsync();

        var controller = new TasksController(context);

        var result = await controller.DeleteTask(1);

        Assert.IsType<NoContentResult>(result);
        Assert.Empty(context.Tasks);
    }

    [Fact]
    public async Task GetTasks_FilterByStatus_ReturnsMatchingTasks()
    {
        using var context = CreateContext();

        context.Tasks.AddRange(
            new TaskItem
            {
                Title = "Pendiente",
                Description = "Descripción",
                States = TaskStates.Pendiente,
                Responsible = "Ana",
                DueDate = DateTime.UtcNow.AddDays(2)
            },
            new TaskItem
            {
                Title = "Hecha",
                Description = "Descripción",
                States = TaskStates.Hecha,
                Responsible = "Luis",
                DueDate = DateTime.UtcNow.AddDays(3)
            }
        );

        await context.SaveChangesAsync();

        var controller = new TasksController(context);

        var result = await controller.GetTasks(
            TaskStates.Pendiente, null, 1, 10);

        var okResult = Assert.IsType<OkObjectResult>(result);

        Assert.NotNull(okResult.Value);
    }
}
