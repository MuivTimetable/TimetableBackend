

namespace TimetableAPI.Models
{
    public class Scheduler
    {
        [Key]
        public int Scheduler_id { get; set; }

        [Required]
        public int Day_id { get; set; }

        [Required]
        public string Branch { get; set; }

        [Required, MaxLength(15)]
        public string Work_start { get; set; }

        [Required, MaxLength(15)]
        public string Work_end { get; set; }

        public string? Area { get; set; }

        [MaxLength(30)]
        public string? Work_type { get; set; }

        public string? Place { get; set; }

        [MaxLength(150)]
        public string? Tutor { get; set; }

        [MaxLength(200)]
        public string? Cathedra { get; set; }

        [MaxLength(250)]
        public string? Comment { get; set; }

        public int Totalizer { get; set; }

        [ForeignKey("Day_id")]
        public SchedulerDate SchedulerDate { get; set; }
    }
}
