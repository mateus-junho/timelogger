using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timelogger.Application.Dtos;
using Timelogger.Application.Interfaces;
using Timelogger.Application.Mappings;
using Timelogger.Domain.DomainObjects;
using Timelogger.Domain.Entities;
using Timelogger.Domain.Interfaces;

namespace Timelogger.Application.Services
{
    public class ProjectAppService : IProjectAppService
    {
        private readonly IProjectRepository projectRepository;

        public ProjectAppService(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjects(bool sortDeadline)
        {
            var projects = await projectRepository.GetAll();
            if (sortDeadline)
            {
                projects = projects.OrderBy(p => p.Deadline);
            }

            return projects.ToDto();
        }

        public async Task<ProjectDto> GetProjectTimeRegister(int projectId)
        {
            var project = await projectRepository.GetById(projectId);

            return project.ToDto();
        }

        public async Task<TimeRegisterDto> RegisterTimeInProject(NewTimeRegisterDto newTimeRegisterDto)
        {
            var project = await projectRepository.GetById(newTimeRegisterDto.ProjectId);

            if (project == null)
            {
                throw new DomainException("Invalid project");
            }

            if (project.IsComplete)
            {
                throw new DomainException("Once a project is complete it can no longer receive new registrations");
            }

            var timeRegister = new TimeRegister(newTimeRegisterDto.ProjectId, newTimeRegisterDto.Start, newTimeRegisterDto.End);
            await projectRepository.AddTimeRegister(timeRegister);

            return timeRegister.ToDto();
        }
    }
}
