using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timelogger.Domain.Entities;
using Timelogger.Domain.Interfaces;

namespace Timelogger.Data
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApiContext context;

        public ProjectRepository(ApiContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            return await context.Projects
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Project> GetById(int projectId)
        {
            return await context.Projects
                .Include(p => p.TimeRegisters)
                .FirstOrDefaultAsync(p => p.Id == projectId);
        }

        public async Task AddTimeRegister(TimeRegister timeRegister)
        {
            await context.TimeRegisters.AddAsync(timeRegister);
            context.SaveChanges();
        }
    }
}
