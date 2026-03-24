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
    public class TeamMembersApiIntegrationTests : TestBase
    {
        [Fact]
        public async Task Get_Members_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/TeamMembersApi");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_Member_CreatesNewMember()
        {
            // Arrange
            var client = Factory.CreateClient();
            var newMember = new TeamMember
            {
                Name = "Integration Test Member",
                Email = "test@integration.com"
            };
            var content = new StringContent(JsonConvert.SerializeObject(newMember), System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/TeamMembersApi", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            
            var resultString = await response.Content.ReadAsStringAsync();
            var createdMember = JsonConvert.DeserializeObject<TeamMember>(resultString);
            Assert.Equal(newMember.Name, createdMember.Name);
        }
    }
}
