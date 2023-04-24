using System;
using Timelogger.Domain.DomainObjects;

namespace Timelogger.Domain.Entities
{
    public class TimeRegister
    {
        public Guid Id { get; private set; }

        public int ProjectId { get; private set; }

        public DateTime Start { get; private set; }

        public DateTime End { get; private set; }

        public Project Project { get; private set; }

        private int MIN_REGISTRATION_TIME_IN_MINUTES = 30;

        protected TimeRegister()
        { }

        public TimeRegister(int projectId, DateTime start, DateTime end)
        {
            Id = Guid.NewGuid();
            ProjectId = projectId;
            Start = start;
            End = end;

            Validate();
        }

        private void Validate()
        {
            if (End.Subtract(Start).TotalMinutes < MIN_REGISTRATION_TIME_IN_MINUTES)
            {
                throw new DomainException("Individual time registrations should be 30 minutes or longer");
            }
        }
    }
}
