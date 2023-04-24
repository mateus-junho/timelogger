using System;

namespace Timelogger.Application.Dtos
{
    public class TimeRegisterDto
    {
        public Guid Id { get; set; }

        public int ProjectId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
