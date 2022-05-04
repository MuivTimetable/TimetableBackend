namespace TimetableAPI.Models
{
    public class Scheduler_Group
    {
        [Key]
        public int Scheduler_id { get; set; }

        [ForeignKey("Scheduler_id")]
        public Scheduler Scheduler { get; set; }

        public int Group_id { get; set; }

        [ForeignKey("Group_id")]
        public Group Group { get; set; }
    }
}
