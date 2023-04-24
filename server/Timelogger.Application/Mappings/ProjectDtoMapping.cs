using System.Collections.Generic;
using System.Linq;
using Timelogger.Application.Dtos;
using Timelogger.Domain.Entities;

namespace Timelogger.Application.Mappings
{
    public static class ProjectDtoMapping
    {
        public static ProjectDto ToDto(this Project project)
        {
            if (project == null)
            {
                return null;
            }

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Deadline = project.Deadline,
                IsComplete = project.IsComplete,
                TimeRegisters = project.TimeRegisters.ToDto(),
            };
        }

        public static IEnumerable<ProjectDto> ToDto(this IEnumerable<Project> projects)
        {
            if (projects == null || !projects.Any())
            {
                return new List<ProjectDto>();
            }

            return projects.Select(t => ToDto(t));
        }
    }
}
