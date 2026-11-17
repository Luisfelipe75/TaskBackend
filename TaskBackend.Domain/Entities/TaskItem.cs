using System.ComponentModel.DataAnnotations;
using TaskBackend.Domain.Enums;

namespace TaskBackend.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;

        public TaskStates States { get; set; } =
            TaskStates.Pendiente;

        [Required]
        [StringLength(120)]
        public string Responsible { get; set; } = string.Empty;

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime CreatedAt { get; set; } =
            DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } =
            DateTime.UtcNow;
    }
}
