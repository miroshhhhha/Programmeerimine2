using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Controllers;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class ProjectsControllerTests
    {
        private readonly Mock<IProjectService> _serviceMock;
        private readonly ProjectsController _controller;

        public ProjectsControllerTests()
        {
            _serviceMock = new Mock<IProjectService>();
            _controller = new ProjectsController(_serviceMock.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewWithPagedResult()
        {
            // Arrange
            var pagedResult = new PagedResult<Project> { Results = new List<Project>() };
            _serviceMock.Setup(s => s.GetProjectsPagedAsync(1, 10, null))
                        .ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(1, null) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, result.Model);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenProjectDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetProjectByIdAsync(It.IsAny<int>()))
                        .ReturnsAsync((Project)null);

            // Act
            var result = await _controller.Details(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsView_WhenProjectExists()
        {
            // Arrange
            var project = new Project { Id = 1, Name = "Test" };
            _serviceMock.Setup(s => s.GetProjectByIdAsync(1))
                        .ReturnsAsync(project);

            // Act
            var result = await _controller.Details(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(project, result.Model);
        }
    }
}
