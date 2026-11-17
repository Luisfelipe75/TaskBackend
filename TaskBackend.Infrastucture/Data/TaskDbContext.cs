using Microsoft.EntityFrameworkCore;
using TaskBackend.Domain.Entities;

namespace TaskBackend.Infrastucture.Data;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

    public DbSet<TaskItem> Tasks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la entidad TaskItem
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasKey(t => t.Id); // Establece Id como clave primaria

            entity.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(t => t.Description)
                .HasMaxLength(500);

            entity.Property(t => t.States)
                .IsRequired()
                .HasConversion<string>() // Guarda el enum como string en la DB
                .HasMaxLength(50);

            entity.Property(t => t.Responsible)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(t => t.DueDate)
                .IsRequired();

            entity.Property(t => t.CreatedAt)
                .IsRequired();

            entity.Property(t => t.UpdatedAt)
                .IsRequired();

            entity.HasIndex(t => t.States); // Índice para búsquedas por estado
            entity.HasIndex(t => t.Responsible); // Índice para búsquedas por responsable
            entity.HasIndex(t => t.DueDate); // Índice para búsquedas por fecha de vencimiento
        });

        // Semilla de datos para probar
        modelBuilder.Entity<TaskItem>().HasData(
            new TaskItem
            {
                Id = 1,
                Title = "Configurar entorno de desarrollo",
                Description = "Instalar .NET SDK, Visual Studio, SQLite, etc.",
                States = Domain.Enums.TaskStates.Pendiente,
                Responsible = "Juan Pérez",
                DueDate = new DateTime(2026, 12, 01),
                CreatedAt = new DateTime(2026, 11, 01),
                UpdatedAt = new DateTime(2026, 11, 01)
            },
            new TaskItem
            {
                Id = 2,
                Title = "Diseñar base de datos",
                Description = "Definir esquemas de tablas para tareas, usuarios.",
                States = Domain.Enums.TaskStates.EnCurso,
                Responsible = "María García",
                DueDate = new DateTime(2026, 12, 10),
                CreatedAt = new DateTime(2026, 11, 02),
                UpdatedAt = new DateTime(2026, 11, 02)
            },
            new TaskItem
            {
                Id = 3,
                Title = "Implementar autenticación JWT",
                Description = "Desarrollar middleware de autenticación y lógica de login.",
                States = Domain.Enums.TaskStates.Hecha,
                Responsible = "Carlos Ruíz",
                DueDate = new DateTime(2026, 10, 30),
                CreatedAt = new DateTime(2026, 10, 20),
                UpdatedAt = new DateTime(2026, 10, 30)
            }
        );
    }
}
