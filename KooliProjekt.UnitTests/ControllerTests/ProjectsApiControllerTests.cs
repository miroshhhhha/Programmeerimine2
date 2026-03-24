using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Controllers.Api;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class ProjectsApiControllerTests
    {
        private readonly Mock<IProjectService> _serviceMock;
        private readonly ProjectsApiController _controller;

        public ProjectsApiControllerTests()
        {
            _serviceMock = new Mock<IProjectService>();
            _controller = new ProjectsApiController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetProjects_ReturnsAllProjects()
        {
            // Arrange
            var projects = new List<Project> { new Project { Id = 1, Name = "P1" } };
            _serviceMock.Setup(s => s.GetAllProjectsAsync()).ReturnsAsync(projects);

            // Act
            var result = await _controller.GetProjects();

            // Assert
            Assert.Equal(projects, result.Value);
        }

        [Fact]
        public async Task GetProject_ReturnsNotFound_WhenDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetProjectByIdAsync(1)).ReturnsAsync((Project)null);

            // Act
            var result = await _controller.GetProject(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostProject_CreatesProject()
        {
            // Arrange
            var project = new Project { Name = "New" };
            _serviceMock.Setup(s => s.AddProjectAsync(project)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PostProject(project);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(project, createdAtActionResult.Value);
        }
    }
}
