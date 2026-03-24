using Xunit;
using KooliProjekt.Services;
using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using KooliProjekt.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class ProjectServiceTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ProjectService _service;

        public ProjectServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            SeedTestData();
            _service = new ProjectService(_context);
        }

        private void SeedTestData()
        {
            _context.Projects.AddRange(
                new Project { Id = 1, Name = "Project A", Description = "Desc A", StartDate = DateTime.Now },
                new Project { Id = 2, Name = "Project B", Description = "Desc B", StartDate = DateTime.Now },
                new Project { Id = 3, Name = "Alpha", Description = "Desc C", StartDate = DateTime.Now }
            );

            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async Task GetProjectByIdAsync_ReturnsProject_WhenExists()
        {
            // Act
            var result = await _service.GetProjectByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Project A", result.Name);
        }

        [Fact]
        public async Task GetProjectByIdAsync_ReturnsNull_WhenNotExists()
        {
            // Act
            var result = await _service.GetProjectByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetProjectsPagedAsync_ReturnsPagedResult()
        {
            // Act
            var result = await _service.GetProjectsPagedAsync(1, 2);

            // Assert
            Assert.Equal(2, result.Results.Count);
            Assert.Equal(3, result.RowCount);
            Assert.Equal(2, result.PageCount);
        }

        [Fact]
        public async Task GetProjectsPagedAsync_FiltersBySearchTerm()
        {
            // Act
            var result = await _service.GetProjectsPagedAsync(1, 10, "Alpha");

            // Assert
            Assert.Single(result.Results);
            Assert.Equal("Alpha", result.Results[0].Name);
        }

        [Fact]
        public async Task AddProjectAsync_CreatesNewProject()
        {
            // Arrange
            var newProject = new Project
            {
                Name = "New Project",
                Description = "New Desc",
                StartDate = DateTime.Now
            };

            // Act
            await _service.AddProjectAsync(newProject);

            // Assert
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Name == "New Project");
            Assert.NotNull(project);
        }

        [Fact]
        public async Task UpdateProjectAsync_UpdatesExistingProject()
        {
            // Arrange
            var project = await _context.Projects.FindAsync(1);
            project.Name = "Updated Name";

            // Act
            await _service.UpdateProjectAsync(project);

            // Assert
            var updated = await _context.Projects.FindAsync(1);
            Assert.Equal("Updated Name", updated.Name);
        }

        [Fact]
        public async Task DeleteProjectAsync_RemovesProject()
        {
            // Act
            await _service.DeleteProjectAsync(1);

            // Assert
            Assert.Null(await _context.Projects.FindAsync(1));
            Assert.Equal(2, await _context.Projects.CountAsync());
        }
    }
}
