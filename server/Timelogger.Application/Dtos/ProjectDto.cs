using System;
using System.Collections.Generic;

namespace Timelogger.Application.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Deadline { get; set; }

        public bool IsComplete { get; set; }

        public IEnumerable<TimeRegisterDto> TimeRegisters { get; set; }
    }
}
