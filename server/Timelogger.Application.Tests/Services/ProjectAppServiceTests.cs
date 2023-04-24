using FizzWare.NBuilder;
using Moq;
using MoqMeUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timelogger.Application.Dtos;
using Timelogger.Application.Interfaces;
using Timelogger.Application.Services;
using Timelogger.Domain.DomainObjects;
using Timelogger.Domain.Entities;
using Timelogger.Domain.Interfaces;
using Xunit;

namespace Timelogger.Application.Tests.Services
{
    public class ProjectAppServiceTests : MoqMeUp<ProjectAppService>
    {
        IProjectAppService projectAppService;

        public ProjectAppServiceTests()
        {
            projectAppService = Build();
        }

        [Fact]
        public async Task GetAllProjects_SortDeadline_ReturnsOrderedList()
        {
            // Arrange
            var shortestDeadline = DateTime.Today.AddDays(30);

            var projects = new List<Project>
            {
                new Project(1, "project 1", shortestDeadline.AddDays(90)),
                new Project(2, "project 2", shortestDeadline)
            };

            this.Get<IProjectRepository>().Setup(x => x.GetAll()).ReturnsAsync(projects);

            // Act
            var act = await projectAppService.GetAllProjects(true);

            // Assert
            Assert.Equal(shortestDeadline, act.First().Deadline);
        }

        [Fact]
        public async Task RegisterTimeInProject_ValidProject_AddTimeRegister()
        {
            // Arrange
            var newTimeRegister = Builder<NewTimeRegisterDto>
                .CreateNew()
                .With(x => x.Start = DateTime.Now)
                .With(x => x.End = DateTime.Now.AddMinutes(40))
                .Build();

            var project = new Project(1, "project 1", DateTime.Today);
            this.Get<IProjectRepository>().Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(project);

            // Act
            var act = await projectAppService.RegisterTimeInProject(newTimeRegister);

            // Assert
            Assert.NotNull(act);
            this.Get<IProjectRepository>().Verify(x => x.AddTimeRegister(It.IsAny<TimeRegister>()), Times.Once);
        }

        [Fact]
        public async Task RegisterTimeInProject_ProjectNotFound_ThrowsException()
        {
            // Arrange
            this.Get<IProjectRepository>().Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((Project)null);

            // Act
            var act = await Record.ExceptionAsync(async () => 
                await projectAppService.RegisterTimeInProject(Builder<NewTimeRegisterDto>.CreateNew().Build()));

            // Assert
            Assert.IsAssignableFrom<DomainException>(act);
        }

        [Fact]
        public async Task RegisterTimeInProject_ProjectComplete_ThrowsException()
        {
            // Arrange
            var project = new Project(1, "project 1", DateTime.Today);
            project.SetComplete();
            this.Get<IProjectRepository>().Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(project);

            // Act
            var act = await Record.ExceptionAsync(async () =>
                await projectAppService.RegisterTimeInProject(Builder<NewTimeRegisterDto>.CreateNew().Build()));

            // Assert
            Assert.IsAssignableFrom<DomainException>(act);
        }
    }
}
