using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Models;
using KooliProjekt.WPF;
using Moq;
using Xunit;

namespace KooliProjekt.WPF.Tests
{
    public class MainViewModelTests
    {
        private readonly Mock<IApiClient> _apiClientMock;
        private readonly MainViewModel _viewModel;

        public MainViewModelTests()
        {
            _apiClientMock = new Mock<IApiClient>();
            _viewModel = new MainViewModel(_apiClientMock.Object);
        }

        [Fact]
        public async Task LoadProjectsAsync_Should_Populate_Projects_On_Success()
        {
            // Arrange
            var projects = new List<Project>
            {
                new Project { Id = 1, Name = "Test Project 1" },
                new Project { Id = 2, Name = "Test Project 2" }
            };
            _apiClientMock.Setup(x => x.GetAsync<Project>("ProjectsApi")).ReturnsAsync(projects);

            // Act
            await _viewModel.LoadProjectsAsync();

            // Assert
            Assert.Equal(2, _viewModel.Projects.Count);
            Assert.Equal("Test Project 1", _viewModel.Projects[0].Name);
            Assert.Null(_viewModel.ErrorMessage);
        }

        [Fact]
        public async Task LoadProjectsAsync_Should_Set_ErrorMessage_On_Failure()
        {
            // Arrange
            _apiClientMock.Setup(x => x.GetAsync<Project>("ProjectsApi")).ThrowsAsync(new Exception("API Error"));

            // Act
            await _viewModel.LoadProjectsAsync();

            // Assert
            Assert.Empty(_viewModel.Projects);
            Assert.Contains("Error loading projects: API Error", _viewModel.ErrorMessage);
        }

        [Fact]
        public async Task AddProjectAsync_Should_Call_PostAsync_And_Reload_On_Success()
        {
            // Arrange
            var project = new Project { Name = "New Project" };
            _apiClientMock.Setup(x => x.PostAsync("ProjectsApi", project)).Returns(Task.CompletedTask);
            _apiClientMock.Setup(x => x.GetAsync<Project>("ProjectsApi")).ReturnsAsync(new List<Project>());

            // Act
            var result = await _viewModel.AddProjectAsync(project);

            // Assert
            Assert.True(result.IsSuccess);
            _apiClientMock.Verify(x => x.PostAsync("ProjectsApi", project), Times.Once);
            _apiClientMock.Verify(x => x.GetAsync<Project>("ProjectsApi"), Times.Once);
        }

        [Fact]
        public async Task AddProjectAsync_Should_Return_Failure_On_Exception()
        {
            // Arrange
            var project = new Project { Name = "New Project" };
            _apiClientMock.Setup(x => x.PostAsync("ProjectsApi", project)).ThrowsAsync(new Exception("Post Error"));

            // Act
            var result = await _viewModel.AddProjectAsync(project);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Post Error", result.Error);
            Assert.Contains("Error adding project: Post Error", _viewModel.ErrorMessage);
        }

        [Fact]
        public async Task DeleteProjectAsync_Should_Call_DeleteAsync_And_Reload_On_Success()
        {
            // Arrange
            int projectId = 1;
            _apiClientMock.Setup(x => x.DeleteAsync("ProjectsApi", projectId)).Returns(Task.CompletedTask);
            _apiClientMock.Setup(x => x.GetAsync<Project>("ProjectsApi")).ReturnsAsync(new List<Project>());

            // Act
            var result = await _viewModel.DeleteProjectAsync(projectId);

            // Assert
            Assert.True(result.IsSuccess);
            _apiClientMock.Verify(x => x.DeleteAsync("ProjectsApi", projectId), Times.Once);
            _apiClientMock.Verify(x => x.GetAsync<Project>("ProjectsApi"), Times.Once);
        }
    }
}
