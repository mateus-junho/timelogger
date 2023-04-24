using System;
using System.Collections.Generic;
using Timelogger.Domain.DomainObjects;

namespace Timelogger.Domain.Entities
{
    public class Project
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public DateTime Deadline { get; private set; }

        public bool IsComplete { get; private set; }

        public List<TimeRegister> TimeRegisters { get; private set; }

        protected Project()
        { }

        public Project(int id, string name, DateTime deadline)
        {
            Id = id;
            Name = name;
            Deadline = deadline;
            IsComplete = false;
            TimeRegisters = new List<TimeRegister>();

            Validate();
        }

        public void SetComplete() => IsComplete = true;

        private void Validate()
        {
            if (Id < 1)
            {
                throw new DomainException("Id should be an integer value");
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new DomainException("Name cannot be empty");
            }
        }
    }
}
