using System.Collections.Generic;
using System.Linq;
using Timelogger.Application.Dtos;
using Timelogger.Domain.Entities;

namespace Timelogger.Application.Mappings
{
    public static class TimeRegisterDtoMapping
    {
        public static TimeRegisterDto ToDto(this TimeRegister timeRegister)
        {
            if (timeRegister == null)
            {
                return null;
            }

            return new TimeRegisterDto
            {
                Id = timeRegister.Id,
                ProjectId = timeRegister.ProjectId,
                Start = timeRegister.Start,
                End = timeRegister.End,
            };
        }

        public static IEnumerable<TimeRegisterDto> ToDto(this IEnumerable<TimeRegister> timeRegisters)
        {
            if (timeRegisters == null || !timeRegisters.Any())
            {
                return new List<TimeRegisterDto>();
            }

            return timeRegisters.Select(t => ToDto(t));
        }
    }
}
