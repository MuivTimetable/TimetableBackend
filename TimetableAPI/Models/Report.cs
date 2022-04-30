namespace TimetableAPI.Models
{
    public class Report
    {
        [Key]
        public int Report_id { get; set; }

        public int? User_id { get; set; }

        [Required,MaxLength(250)]
        public string Message { get; set; }

        [Required]
        public DateTime report_date { get; set; }
    }
}
