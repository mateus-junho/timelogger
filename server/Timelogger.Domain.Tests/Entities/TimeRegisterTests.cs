using System;
using Timelogger.Domain.DomainObjects;
using Timelogger.Domain.Entities;
using Xunit;

namespace Timelogger.Domain.Tests.Entities
{
    public class TimeRegisterTests
    {
        [Fact]
        public void TimeRegister_Validate()
        {
            var ex = Assert.Throws<DomainException>(() =>
                new TimeRegister(1, DateTime.Now, DateTime.Now.AddMinutes(28)) 
            );
            Assert.Equal("Individual time registrations should be 30 minutes or longer", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new TimeRegister(1, DateTime.Now.AddMinutes(-2), DateTime.Now)
            );
            Assert.Equal("Individual time registrations should be 30 minutes or longer", ex.Message);
        }
    }
}
