using System;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string Priority { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Deadline { get; set; }

        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public int? TeamMemberId { get; set; }
        public TeamMember? AssignedMember { get; set; }
    }
}
