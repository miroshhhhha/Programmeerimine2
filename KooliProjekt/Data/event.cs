using Microsoft.AspNetCore.Identity;

namespace KooliProjekt.Data
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public IList<Plan> Schedule { get; set; }
        public IList<IdentityUser> Participants { get; set; }
    }
}
