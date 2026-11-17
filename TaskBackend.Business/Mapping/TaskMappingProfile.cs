
using AutoMapper;
using TaskBackend.Domain.DTOs;
using TaskBackend.Domain.Entities;

namespace TaskBackend.Business.Mapping;

public class TaskMappingProfile : Profile
{
    public TaskMappingProfile()
    {
        // Entidad -> DTO de respuesta
        CreateMap<TaskItem, TaskResponseDto>();

        // DTO de creación -> Entidad
        CreateMap<CreateTaskDto, TaskItem>()
            .ForMember(
                dest => dest.Id,
                opt => opt.Ignore()
            )
            .ForMember(
                dest => dest.CreatedAt,
                opt => opt.MapFrom(_ => DateTime.UtcNow)
            )
            .ForMember(
                dest => dest.UpdatedAt,
                opt => opt.MapFrom(_ => DateTime.UtcNow)
            );

        // DTO de actualización -> Entidad
        CreateMap<UpdateTaskDto, TaskItem>()
            .ForMember(
                dest => dest.Id,
                opt => opt.Ignore()
            )
            .ForMember(
                dest => dest.CreatedAt,
                opt => opt.Ignore()
            )
            .ForMember(
                dest => dest.UpdatedAt,
                opt => opt.MapFrom(_ => DateTime.UtcNow)
            );
    }
}
