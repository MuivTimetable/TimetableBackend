

namespace TimetableAPI.Models
{
    public class Scheduler
    {
        [Key]
        public int Scheduler_id { get; set; }

        [Required]
        public int Day_id { get; set; }

        [Required, MaxLength(50)]
        public string Branch { get; set; }

        [Required, MaxLength(10)]
        public string Work_start { get; set; }

        [Required, MaxLength(10)]
        public string Work_end { get; set; }

        [MaxLength(100)]
        public string? Area { get; set; }

        [MaxLength(30)]
        public string? Work_type { get; set; }

        public string? Place { get; set; }

        [MaxLength(50)]
        public string? Tutor { get; set; }

        [MaxLength(50)]
        public string? Cathedra { get; set; }

        [MaxLength(250)]
        public string? Comment { get; set; }

        public int Totalizer { get; set; }

        [ForeignKey("Day_id")]
        public SchedulerDate SchedulerDate { get; set; }
    }
}
