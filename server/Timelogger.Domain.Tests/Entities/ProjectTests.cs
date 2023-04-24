using System;
using Timelogger.Domain.DomainObjects;
using Timelogger.Domain.Entities;
using Xunit;

namespace Timelogger.Domain.Tests.Entities
{
    public class ProjectTests
    {
        [Fact]
        public void Project_Validate()
        {
            var ex = Assert.Throws<DomainException>(() =>
                new Project(0, "new project test", DateTime.Today)
            );
            Assert.Equal("Id should be an integer value", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Project(5, "     ", DateTime.Today)
            );
            Assert.Equal("Name cannot be empty", ex.Message);
        }
    }
}
