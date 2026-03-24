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
    public class ProjectsApiIntegrationTests : TestBase
    {
        [Fact]
        public async Task Get_Projects_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/ProjectsApi");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_Project_CreatesNewProject()
        {
            // Arrange
            var client = Factory.CreateClient();
            var newProject = new Project
            {
                Name = "Integration Test Project",
                Description = "Description",
                StartDate = System.DateTime.Now
            };
            var content = new StringContent(JsonConvert.SerializeObject(newProject), System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/ProjectsApi", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            
            var resultString = await response.Content.ReadAsStringAsync();
            var createdProject = JsonConvert.DeserializeObject<Project>(resultString);
            Assert.Equal(newProject.Name, createdProject.Name);
        }
    }
}
