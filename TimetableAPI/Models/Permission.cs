namespace TimetableAPI.Models
{
    public class Permission
    {
        [Key]
        public int Permission_id { get; set; }

        [Required,MaxLength(30)]
        public string Permission_name { get; set; }
    }
}
