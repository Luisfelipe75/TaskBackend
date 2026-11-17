
using System.ComponentModel.DataAnnotations;
using TaskBackend.Domain.Enums;

namespace TaskBackend.Domain.DTOs;

public class CreateTaskDto
{
    [Required]
    [StringLength(120, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;

    [EnumDataType(typeof(TaskStates))]
    public TaskStates States { get; set; } =
        TaskStates.Pendiente;

    [Required]
    [StringLength(120, MinimumLength = 1)]
    public string Responsible { get; set; } = string.Empty;

    [Required]
    public DateTime? DueDate { get; set; }
}
