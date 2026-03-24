using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

namespace KooliProjekt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<KooliProjekt.Models.Project> Projects { get; set; }
        public DbSet<KooliProjekt.Models.ProjectTask> ProjectTasks { get; set; }
        public DbSet<KooliProjekt.Models.TeamMember> TeamMembers { get; set; }
    }
}