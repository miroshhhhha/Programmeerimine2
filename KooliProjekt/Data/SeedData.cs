using Microsoft.AspNetCore.Identity;
using KooliProjekt.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KooliProjekt.Data
{
    public static class SeedData
    {
        public static void Generate(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            if (context.Projects.Any())
            {
                return;
            }

            var adminUser = new IdentityUser
            {
                UserName = "admin@project.com",
                Email = "admin@project.com",
                EmailConfirmed = true,
            };

            if (userManager.FindByEmailAsync(adminUser.Email).GetAwaiter().GetResult() == null)
            {
                userManager.CreateAsync(adminUser, "Password123!").GetAwaiter().GetResult();
            }

            var members = new List<TeamMember>
            {
                new TeamMember { Name = "Alice Johnson", Email = "alice@example.com" },
                new TeamMember { Name = "Bob Smith", Email = "bob@example.com" },
                new TeamMember { Name = "Charlie Brown", Email = "charlie@example.com" }
            };
            context.TeamMembers.AddRange(members);
            context.SaveChanges();

            for (int i = 1; i <= 15; i++)
            {
                var project = new Project
                {
                    Name = $"Project {i}",
                    Description = $"Description for project {i}",
                    StartDate = DateTime.Now.AddDays(-i),
                    EndDate = DateTime.Now.AddDays(30 + i)
                };

                context.Projects.Add(project);
                context.SaveChanges();

                var tasks = new List<ProjectTask>
                {
                    new ProjectTask 
                    { 
                        Title = $"Task 1 for {project.Name}", 
                        Description = "High priority task", 
                        Status = "InProgress", 
                        Priority = "High", 
                        Deadline = DateTime.Now.AddDays(7),
                        ProjectId = project.Id,
                        TeamMemberId = members[i % 3].Id
                    },
                    new ProjectTask 
                    { 
                        Title = $"Task 2 for {project.Name}", 
                        Description = "Standard task", 
                        Status = "New", 
                        Priority = "Medium", 
                        Deadline = DateTime.Now.AddDays(14),
                        ProjectId = project.Id,
                        TeamMemberId = members[(i+1) % 3].Id
                    }
                };
                context.ProjectTasks.AddRange(tasks);
            }

            context.SaveChanges();
        }
    }
}
