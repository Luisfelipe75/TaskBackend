using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TaskBackend.Infrastucture.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()
        );
    })
    // Se añade explícitamente el ensamblado actual como una Application Part.
    // Esto asegura que los controladores definidos en este ensamblado (TaskBackend.API)
    // sean descubiertos por el sistema de endpoints y, por ende, por Swagger.
    .AddApplicationPart(typeof(Program).Assembly);

//Garantiza que las routas se interpreten en minusculas en el navegador
builder.Services.AddRouting(routing => routing.LowercaseUrls = true);

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails();

builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);


var app = builder.Build();

app.UseExceptionHandler();

// Habilitar Swagger en entornos de desarrollo 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class Program { }
