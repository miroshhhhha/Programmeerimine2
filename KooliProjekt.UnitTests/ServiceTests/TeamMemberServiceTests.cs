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
    public class TeamMemberServiceTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly TeamMemberService _service;

        public TeamMemberServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            SeedTestData();
            _service = new TeamMemberService(_context);
        }

        private void SeedTestData()
        {
            _context.TeamMembers.AddRange(
                new TeamMember { Id = 1, Name = "Alice", Email = "alice@example.com" },
                new TeamMember { Id = 2, Name = "Bob", Email = "bob@example.com" },
                new TeamMember { Id = 3, Name = "Charlie", Email = "charlie@example.com" }
            );

            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async Task GetMemberByIdAsync_ReturnsMember_WhenExists()
        {
            // Act
            var result = await _service.GetMemberByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Alice", result.Name);
        }

        [Fact]
        public async Task GetMembersPagedAsync_ReturnsPagedResult()
        {
            // Act
            var result = await _service.GetMembersPagedAsync(1, 2);

            // Assert
            Assert.Equal(2, result.Results.Count);
            Assert.Equal(3, result.RowCount);
        }

        [Fact]
        public async Task GetMembersPagedAsync_FiltersBySearchTerm()
        {
            // Act
            var result = await _service.GetMembersPagedAsync(1, 10, "Bob");

            // Assert
            Assert.Single(result.Results);
            Assert.Equal("Bob", result.Results[0].Name);
        }

        [Fact]
        public async Task AddMemberAsync_CreatesNewMember()
        {
            // Arrange
            var newMember = new TeamMember
            {
                Name = "Dave",
                Email = "dave@example.com"
            };

            // Act
            await _service.AddMemberAsync(newMember);

            // Assert
            var member = await _context.TeamMembers.FirstOrDefaultAsync(m => m.Name == "Dave");
            Assert.NotNull(member);
        }

        [Fact]
        public async Task UpdateMemberAsync_UpdatesExistingMember()
        {
            // Arrange
            var member = await _context.TeamMembers.FindAsync(1);
            member.Name = "Alice Smith";

            // Act
            await _service.UpdateMemberAsync(member);

            // Assert
            var updated = await _context.TeamMembers.FindAsync(1);
            Assert.Equal("Alice Smith", updated.Name);
        }

        [Fact]
        public async Task DeleteMemberAsync_RemovesMember()
        {
            // Act
            await _service.DeleteMemberAsync(1);

            // Assert
            Assert.Null(await _context.TeamMembers.FindAsync(1));
            Assert.Equal(2, await _context.TeamMembers.CountAsync());
        }
    }
}
