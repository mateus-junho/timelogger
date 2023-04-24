using System.Collections.Generic;
using System.Threading.Tasks;
using Timelogger.Domain.Entities;

namespace Timelogger.Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAll();

        Task<Project> GetById(int projectId);

        Task AddTimeRegister(TimeRegister timeRegister);
    }
}
