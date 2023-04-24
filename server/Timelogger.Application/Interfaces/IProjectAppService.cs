using System.Collections.Generic;
using System.Threading.Tasks;
using Timelogger.Application.Dtos;

namespace Timelogger.Application.Interfaces
{
    public interface IProjectAppService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjects(bool sortDeadline);

        Task<ProjectDto> GetProjectTimeRegister(int projectId);

        Task<TimeRegisterDto> RegisterTimeInProject(NewTimeRegisterDto newTimeRegisterDto);
    }
}
