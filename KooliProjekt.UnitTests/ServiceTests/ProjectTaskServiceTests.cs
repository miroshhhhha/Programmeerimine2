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
    public class ProjectTaskServiceTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ProjectTaskService _service;

        public ProjectTaskServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            SeedTestData();
            _service = new ProjectTaskService(_context);
        }

        private void SeedTestData()
        {
            var project = new Project { Id = 1, Name = "Project A", Description = "Test Description", StartDate = DateTime.Now };
            _context.Projects.Add(project);

            _context.ProjectTasks.AddRange(
                new ProjectTask { Id = 1, Title = "Task A", Description = "Desc A", ProjectId = 1, Status = "New", Priority = "High" },
                new ProjectTask { Id = 2, Title = "Task B", Description = "Desc B", ProjectId = 1, Status = "InProgress", Priority = "Medium" },
                new ProjectTask { Id = 3, Title = "Bug Fix", Description = "Desc C", ProjectId = 1, Status = "Completed", Priority = "Low" }
            );

            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async Task GetTaskByIdAsync_ReturnsTask_WhenExists()
        {
            // Act
            var result = await _service.GetTaskByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Task A", result.Title);
        }

        [Fact]
        public async Task GetTasksPagedAsync_ReturnsPagedResult()
        {
            // Act
            var result = await _service.GetTasksPagedAsync(1, 2);

            // Assert
            Assert.Equal(2, result.Results.Count);
            Assert.Equal(3, result.RowCount);
        }

        [Fact]
        public async Task GetTasksPagedAsync_FiltersBySearchTerm()
        {
            // Act
            var result = await _service.GetTasksPagedAsync(1, 10, "Bug");

            // Assert
            Assert.Single(result.Results);
            Assert.Equal("Bug Fix", result.Results[0].Title);
        }

        [Fact]
        public async Task AddTaskAsync_CreatesNewTask()
        {
            // Arrange
            var newTask = new ProjectTask
            {
                Title = "New Task",
                Description = "New Desc",
                ProjectId = 1,
                Status = "New",
                Priority = "Medium"
            };

            // Act
            await _service.AddTaskAsync(newTask);

            // Assert
            var task = await _context.ProjectTasks.FirstOrDefaultAsync(t => t.Title == "New Task");
            Assert.NotNull(task);
        }

        [Fact]
        public async Task UpdateTaskAsync_UpdatesExistingTask()
        {
            // Arrange
            var task = await _context.ProjectTasks.FindAsync(1);
            task.Title = "Updated Task Title";

            // Act
            await _service.UpdateTaskAsync(task);

            // Assert
            var updated = await _context.ProjectTasks.FindAsync(1);
            Assert.Equal("Updated Task Title", updated.Title);
        }

        [Fact]
        public async Task DeleteTaskAsync_RemovesTask()
        {
            // Act
            await _service.DeleteTaskAsync(1);

            // Assert
            Assert.Null(await _context.ProjectTasks.FindAsync(1));
            Assert.Equal(2, await _context.ProjectTasks.CountAsync());
        }
    }
}
