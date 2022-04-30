

namespace TimetableAPI.Models
{
    public class Scheduler
    {
        public int Scheduler_id { get; set; }

        [Required]
        public string Work_start { get; set; }

        [Required]
        public string Work_end { get; set; }

        [Required]
        public string Area { get; set; }

        public string? Work_type { get; set; }

        public string? Place { get; set; }

        public string? Tutor { get; set; }

        public string? Cathedra { get; set; }

        [Required]
        public int Group_id { get; set; }

        public string? Comment { get; set; }

        public int Totalizer { get; set; }

        [Required]
        public int Day_id { get; set; }

        [ForeignKey("Day_id")]
        public SchedulerDate SchedulerDate { get; set; }
    }
}
