namespace TimetableAPI.Models
{
    public class Scheduler_User_Totalizer
    {
        [Key]
        [Column(Order = 0)]
        public int Scheduler_id { get; set; }

        [ForeignKey("Scheduler_id")]
        public Scheduler Scheduler { get; set; }

        [Key]
        [Column(Order = 1)]
        public int User_id { get; set; }

        [ForeignKey("User_id")]
        public User User { get; set; }

        [Required]
        public bool TotalizerOption { get; set; }
    }
}
