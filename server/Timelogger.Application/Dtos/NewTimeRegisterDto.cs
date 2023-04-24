using System;

namespace Timelogger.Application.Dtos
{
    public class NewTimeRegisterDto
    {
        public int ProjectId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
