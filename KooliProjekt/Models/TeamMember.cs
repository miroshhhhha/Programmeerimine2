using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Models
{
    public class TeamMember
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public List<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}
