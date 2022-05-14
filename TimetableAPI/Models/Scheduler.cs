

namespace TimetableAPI.Models
{
    public class Scheduler
    {
        [Key]
        public int Scheduler_id { get; set; }

        [Required]
        public int Day_id { get; set; }

        [Required]
        public int Group_id { get; set; }

        [Required]
        public string Branch { get; set; }

        [Required]
        public string Work_start { get; set; }

        [Required]
        public string Work_end { get; set; }

        public string? Area { get; set; }

        public string? Work_type { get; set; }

        public string? Place { get; set; }

        public string? Tutor { get; set; }

        public string? Cathedra { get; set; }

        public string? Comment { get; set; }

        public int Totalizer { get; set; }

        [ForeignKey("Day_id")]
        public SchedulerDate SchedulerDate { get; set; }

        [ForeignKey("Group_id")]
        public Group Group { get; set; }
    }
}
