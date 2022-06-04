namespace TimetableAPI.Models
{
    public class Session
    {
        [Key]
        [Column(Order = 0)]
        public int User_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public  string SessionIdentificator { get; set; }

        [ForeignKey("User_id")]
        public User User { get; set; }
    }
}
