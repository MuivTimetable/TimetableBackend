namespace TimetableAPI.Models
{
    public class Permission
    {
        [Key]
        public int Permission_id { get; set; }

        [Required]
        public string Permission_name { get; set; }
    }
}
