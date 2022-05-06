namespace TimetableAPI.Models
{
    public class Permission
    {
        [Key]
        public int Permission_id { get; set; }

        [Required,MaxLength(20)]
        public string Permission_name { get; set; }
    }
}
