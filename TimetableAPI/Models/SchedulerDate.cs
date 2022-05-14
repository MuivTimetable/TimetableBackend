namespace TimetableAPI.Models
{
    public class SchedulerDate
    {
        [Key]
        public int Day_id { get; set; }
        
        [Required]
        public int Work_Year { get; set; }

        [Required]
        public int Work_Month { get; set; }

        [Required]
        public int Work_Day { get; set; }

        [MaxLength(30)]
        public string? Work_Date_Name { get; set; }
    }
}
