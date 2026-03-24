using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Models;
using KooliProjekt.IntegrationTests.Helpers;
using Newtonsoft.Json;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    public class ProjectTasksApiIntegrationTests : TestBase
    {
        [Fact]
        public async Task Get_Tasks_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/ProjectTasksApi");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_Task_CreatesNewTask()
        {
            // Arrange
            var client = Factory.CreateClient();
            
            // First, we need a project to attach the task to
            var project = new Project { Name = "Task Project", Description = "Desc", StartDate = System.DateTime.Now };
            var projContent = new StringContent(JsonConvert.SerializeObject(project), System.Text.Encoding.UTF8, "application/json");
            var projResponse = await client.PostAsync("/api/ProjectsApi", projContent);
            var createdProj = JsonConvert.DeserializeObject<Project>(await projResponse.Content.ReadAsStringAsync());

            var newTask = new ProjectTask
            {
                Title = "Integration Test Task",
                Description = "Description",
                Status = "New",
                Priority = "High",
                ProjectId = createdProj.Id
            };
            var content = new StringContent(JsonConvert.SerializeObject(newTask), System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/ProjectTasksApi", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            
            var resultString = await response.Content.ReadAsStringAsync();
            var createdTask = JsonConvert.DeserializeObject<ProjectTask>(resultString);
            Assert.Equal(newTask.Title, createdTask.Title);
        }
    }
}
