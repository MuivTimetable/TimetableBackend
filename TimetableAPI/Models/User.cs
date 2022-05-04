namespace TimetableAPI.Models
{
    public class User
    {
        [Key]
        public int User_id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public int? Group_id { get; set; }

        public string? Token { get; set; }

        public int? AuthCode { get; set; }

        public int? Permission_id { get; set; }

        [ForeignKey("Group_id")]
        public Group Group { get; set; }

        [ForeignKey("Permission_id")]
        public Permission Permission { get; set; }
    }
}
