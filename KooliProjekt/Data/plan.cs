namespace KooliProjekt.Data
{
    public class Plan
    {
        public int Id { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public int PeopleAmount { get; set; }
        public string Descripton { get; set; }

        public Event Event { get; set; }
        public int EventId { get; set; }
    }
}
