namespace TimetableAPI.Models
{
    public class SchedulerDate
    {
        [Key]
        public int Day_id { get; set; }
        
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Work_day { get; set; }
    }
}
